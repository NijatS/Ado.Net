using System.Data.SqlClient;

string connection = "Server=DESKTOP-NIJAT;Database = BoltFood;Trusted_Connection=True;";

SqlConnection sqlconnection = new SqlConnection(connection);

bool continueMenu = true;

while (continueMenu)
{
    ShowMenu();
    Console.Write("Please select step :");
    int.TryParse(Console.ReadLine(), out int step);
    switch (step)
    {
        case 1:
            Console.Clear();
            CreateProduct();
        break;
        case 2:
            Console.Clear();
            GetAllProducts();
        break;
        case 3:
            Console.Clear();
            GetProductById();
            break;
        case 4:
            Console.Clear();
            UpdateProduct();
            break;
        case 5:
            Console.Clear();
            RemoveProduct();
            break;
        case 6:
            Console.Clear();
            CreateCategory();
            break;
        case 7:
            Console.Clear();
            GetCategories();
            break;
        case 8:
            Console.Clear();
            GetCategoryById();
            break;
        case 9:
            Console.Clear();
            UpdateCategory();
            break;
        case 0:
            Console.WriteLine("Closing program...");
            Thread.Sleep(2000);
            continueMenu = false;
            break;
        default:
            Console.Clear();
            Console.WriteLine("Enter valid Number");
            break;


    }
}
void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("1.Create Product\n" +
        "2.Show All Product\n" +
        "3.Show Product By Id\n" +
        "4.Update Product\n" +
        "5.Remove Product\n" +
        "6.Create Category\n" +
        "7.Show All Categories with Products\n" +
        "8.Show Category By Id\n" +
        "9.Update Category");
}
void CreateProduct()
{
    Console.Write("Product Name : ");
    string name = Console.ReadLine();
    Console.Write("Price : ");
    double.TryParse(Console.ReadLine(), out double price);
    Console.Write("Category ID : ");
    int.TryParse(Console.ReadLine(), out int categoryId);

    SqlCommand cmd = new SqlCommand($"Insert into Products Values('{name}','Delicious',{price},{categoryId})", sqlconnection);
    sqlconnection.Open();
    cmd.ExecuteNonQuery();
    sqlconnection.Close();
}
void GetAllProducts()
{
    SqlCommand cmd = new SqlCommand("Select * from Products",sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();

    while (result.Read())
    {
        Console.WriteLine(result["Id"]+" " + result["Name"]+" " + result["Description"] + " " + result["Price"]);
    }
    sqlconnection.Close();
}
void GetProductById()
{
    Console.Write("Id : ");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd = new SqlCommand($"Select * from Products where Id = {Id}", sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    bool status = false;
    while (result.Read())
    {
        Console.WriteLine(result["Id"] + " " + result["Name"] + " " + result["Description"] + " " + result["Price"]);
        status = true;
    }
    if (!status)
    {
        Console.WriteLine("Product is not found...");
    }
    sqlconnection.Close();
}
void UpdateProduct()
{
    Console.Write("Id : ");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd = new SqlCommand($"Select * from Products where Id = {Id}", sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    if (result.Read())
    {
        sqlconnection.Close();
        sqlconnection.Open();
        Console.Write("New Product Name : ");
        string name = Console.ReadLine();
        Console.Write("Price : ");
        double.TryParse(Console.ReadLine(), out double price);
        Console.Write("Category ID : ");
        int.TryParse(Console.ReadLine(), out int categoryId);
        SqlCommand cmd1 = new SqlCommand($"Update Products set Name='{name}',Price='{price}',CategoryId='{categoryId}' where Id = '{Id}'", sqlconnection);
        cmd1.ExecuteNonQuery();
        Console.WriteLine("Updating...");
    }
    else 
    {
        Console.WriteLine("Product is not found...");
    }
    sqlconnection.Close();
}
void RemoveProduct()
{
    Console.Write("Id : ");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd = new SqlCommand($"Select * from Products where Id = '{Id}'", sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    if (result.Read())
    {
        sqlconnection.Close();
        sqlconnection.Open();
        SqlCommand cmd1 = new SqlCommand($"Delete  from Products where Id = '{Id}'", sqlconnection);
        cmd1.ExecuteNonQuery();
        Console.WriteLine("Silirem Qaqa..");
    }
    else 
    {
        Console.WriteLine("Product is not found...");
    }
    sqlconnection.Close();
}
void CreateCategory()
{
    Console.Write("Category Name : ");
    string name = Console.ReadLine();

    SqlCommand cmd = new SqlCommand($"Insert into Category Values('{name}')", sqlconnection);
    sqlconnection.Open();
    cmd.ExecuteNonQuery();
    sqlconnection.Close();
}
void GetCategoryById()
{
    Console.Write("Id : ");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd = new SqlCommand($"Select c.Id 'Category Id', c.Name 'Category Name', p.Name 'Product Name', p.Price from Products p inner join Category c on p.CategoryId = c.Id where c.Id={Id};", sqlconnection);

    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    bool status = false;
    while (result.Read())
    {
        Console.WriteLine("Category: " + result["Category Id"] + " " + result["Category Name"] + " " + " Product : " + result["Product Name"] + " " + "  Price : " + result["Price"]);
        status = true;
    }
    if (!status)
    {
        Console.WriteLine("Category is not found...");
    }
    sqlconnection.Close();
}
void UpdateCategory()
{
    Console.Write("Id : ");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd = new SqlCommand($"Select * from Category where Id = {Id}", sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    if (result.Read())
    {
        sqlconnection.Close();
        sqlconnection.Open();
        Console.Write("New Category Name : ");
        string name = Console.ReadLine();
        SqlCommand cmd1 = new SqlCommand($"Update Category set Name='{name}' where Id = '{Id}'", sqlconnection);
        cmd1.ExecuteNonQuery();
        Console.WriteLine("Updating...");
    }
    else 
    {
        Console.WriteLine("Category is not found...");
    }
    sqlconnection.Close();
}
void GetCategories()
{
    SqlCommand cmd = new SqlCommand("Select c.Id 'Category Id', c.Name 'Category Name', p.Name 'Product Name', p.Price from Products p right join Category c on p.CategoryId = c.Id;", sqlconnection);
    sqlconnection.Open();
    var result = cmd.ExecuteReader();
    while (result.Read())
    {
        Console.WriteLine("Category: " + result["Category Id"] + " " + result["Category Name"] + " " + " Product : " +  result["Product Name"] + " " + "  Price : "+ result["Price"]);
    }
    sqlconnection.Close();

}