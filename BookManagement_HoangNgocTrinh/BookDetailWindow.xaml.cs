using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;

using System.Windows;


namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for BookDetailWindow.xaml
    /// </summary>
    public partial class BookDetailWindow : Window
    {

        private CategoriesServices _cateService = new();
        private BookServices _bookServices = new();
        // ta khai báo thêm 1 biến / property để hứng / chứa cuốn sách cần edit
        // cần update khi user chọn 1 cuốn bên grid ở Main window
        // biến này sẽ được set value = dòng selected bên grid
        // trong mode edit
        // còn mode new mới cuốn sách, biến này sẽ mang NULL
        // BIẾN NÀY GỌI LÀ BIẾN FLAG / PHẤT CỜ KIỂM SOÁT TRẠNG THÁI DATA / MÀN HÌNH - CỘT STATUS
        // FULLPROP HOẶC PROP (AUTO GENERATED PROP)
        public Book SelectedBook { get; set; } = null; // _selectedBook tự sinh ra
                                                       // _selectedBook tự sinh ra		= null
                                                       // int yob = 2004 -> hàm Set() value cho 1 biến
                                                       // cw(yob) -> hàm Get() lấy giá trị của biến
                                                       // nếu tạo mới sách, ko cần quan tấm biến này
                                                       // nếu edit sách, biến này phải có value bên Grid gửi sang
        public BookDetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Đổ categories vào đây
            BookCategoryIdComboBox.ItemsSource = _cateService.GetAllCategories();
            //Treo đầu dê bán thịt heo
            //Show là type nhưng lấy id để làm FK vì ta cần catID
            BookCategoryIdComboBox.DisplayMemberPath = "BookGenreType"; //Show cột nào
            BookCategoryIdComboBox.SelectedValuePath = "BookCategoryId"; //Lấy value thực sự ở đâu
                                                                         // KIỂM TRA XEM ĐÂY LÀ MODE EDIT HAY CREATE
                                                                         // THÔNG QUA BIẾN FLAG SELECTED BOOK
            BookModeLabel.Content = "create".ToUpper();
            if (SelectedBook != null)
            {
                // đổi label thông báo của window tùy theo mode của window
                BookModeLabel.Content = "update".ToUpper();
                // đổ từng ô nhập, disable ô ID, CẤM EDIT ID
                //...
                //MessageBox.Show($"SELECTED BOOK {SelectedBook.BookName} | {SelectedBook.BookId}");
                BookIdTextBox.Text = SelectedBook.BookId.ToString();
                BookIdTextBox.IsReadOnly = true;
                BookNameTextBox.Text = SelectedBook.BookName;
                DescriptionTextBox.Text = SelectedBook.Description;
                PublicationDateDatePicker.Text = SelectedBook.PublicationDate.ToString();
                QuantityTextBox.Text = SelectedBook.Quantity.ToString();
                PriceTextBox.Text = SelectedBook.Price.ToString();
                AuthorTextBox.Text = SelectedBook.Author;
                // QUAN TRỌNG, ĐỪNG QUÊN JUMP LÀ NHẢY ĐẾN ĐÚNG CÁI CATEOGRY
                // MÀ CUỐN SÁCH ĐANG THUỘC VỀ
                BookCategoryIdComboBox.SelectedValue = SelectedBook.BookCategoryId;

            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Book x = new();
            x.BookId = int.Parse(BookIdTextBox.Text);
            x.BookName = BookNameTextBox.Text;
            x.Description = DescriptionTextBox.Text;
            x.PublicationDate = ((DateTime)PublicationDateDatePicker.SelectedDate);
            x.Quantity = int.Parse(QuantityTextBox.Text);
            x.Price = double.Parse(PriceTextBox.Text);
            x.Author = AuthorTextBox.Text;
            x.BookCategoryId = int.Parse(BookCategoryIdComboBox.SelectedValue.ToString());
            // check mode gọi đúng hàm service
            // TODO: BAT VALIDATION CHO CAC O NHAP
            // REQUIRED 1 O TEXT: IS NULL OR EMPTY (WPF)

            // BAT SO NAM TRONG KHOANG, CHAC CHANC IF PHAI CONVERT

            int quantity = int.Parse(QuantityTextBox.Text);
            if (quantity <= 0 || quantity >= 4_000_000)
            {
                MessageBox.Show("The quantity must be between 1...");
            }
           
            if (SelectedBook != null)
            {
                _bookServices.UpdateBook(x);
                MessageBox.Show("Update successfully!");
                this.Close();
                return;
            }
            _bookServices.AddBook(x);
            MessageBox.Show("Book added successfully!");
            this.Close();
            //Mh chính phải F5

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
