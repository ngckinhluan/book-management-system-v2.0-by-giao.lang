using Microsoft.VisualBasic.ApplicationServices;
using Repositories.Entities;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookServices _service = new();
        private UserAccount _currentUser;
        public MainWindow(UserAccount user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private bool IsAdmin()
        {
             return _currentUser.Role == 1; 
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                //Mở MH trống để tạo sách
                BookDetailWindow bookDetail = new();
                //Render
                bookDetail.ShowDialog(); //Kẹt
                                         //F5 Grid
                LoadDataGrid();
            } else
            {
                MessageBox.Show("You have no permission to do this!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (IsAdmin())
            {
                //nếu n dùng chọn 1 dòng trong grid, ta có 1 obj mà thực ra là 1 book từ table, nay trong ram của services
                //convert nó thành Book và đẩy sang form detail để GetName, GetPrice...
                Book selected = BookListDataGrid.SelectedItem as Book;
                // = (Book) BookListDataGrid.SelectedItems
                //Ép kiểu này dễ bị Exc
                //Còn kiểu này ép ko đc thì null
                if (selected == null)
                {
                    MessageBox.Show("Please select a book for updated!", "Select one", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // đoạn này có sẵn 1 cuốn sách trong ram đang được trỏ bởi selected
                // gán cuốn này sang cửa sổ màn hình detail qua prop SelectedBook biến
                BookDetailWindow detail = new();
                // trước khi render thì chuyển cuốn sách đang chọn trong Grid sang bên kia
                detail.SelectedBook = selected;
                detail.ShowDialog();
                // F5 Grid đề phòng bà con update info sách, grid phải có info mới của cuốn sách 
                LoadDataGrid();

                //if (selected != null)
                //{
                //    MessageBox.Show($"{selected.BookId} | {selected.BookName}");
                //}
            } else
            {
                MessageBox.Show("You have no permission to do this!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



        }

        //Khi màn hình render, hàm này đc gọi và đổ data vào grid thay vì phải bấm
        //Lưu ý: đổ data vào grid sẽ phải làm n lần
        //MH mở lên
        //Xóa cuốn sách (F5)
        //Update 1 cuốn sách (F5)
        //Thêm sách (F5)
        //Tách việc đổ lưới thành 1 nơi, gọi lại ở chỗ khác để giúp code dễ đọc
        private void BookMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Notification.Content = $"Hello {_currentUser.FullName}";
            LoadDataGrid();
        }

        //Hàm helper
        private void LoadDataGrid()
        {
            BookListDataGrid.ItemsSource = null; //Xóa lưới
            BookListDataGrid.ItemsSource = _service.GetAllBooks();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                var selectedBook = BookListDataGrid.SelectedItem as Book;
                if (selectedBook == null)
                {
                    MessageBox.Show("Please select a book to delete!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Do you want to delete this book?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Book deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    _service.DeleteBook(selectedBook);
                    LoadDataGrid();
                }
                else
                {
                    this.Close();
                }
            } else
            {
                MessageBox.Show("You have no permission to do this!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
         
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = BookNameTextBox.Text;
            string descriptionText = DescriptionTextBox.Text;

            //var searchResults = _service.SearchBooks(searchText, descriptionText);
            var searchResults2 = _service.SearchByNameAndDescription(searchText, descriptionText);
            //var searchResults3 = _service.SearchByNameOrDescription(searchText, descriptionText);
            BookListDataGrid.ItemsSource = searchResults2;
        }



        private void BookNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BookListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}