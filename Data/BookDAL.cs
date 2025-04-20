using MySql.Data.MySqlClient;

public class BookDAL
{
    private readonly string _connectionString;

    public BookDAL(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<Book> GetAll()
    {
        var list = new List<Book>();
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var cmd = new MySqlCommand("SELECT * FROM Books", con);
        using var rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            list.Add(new Book
            {
                Id = rdr.GetInt32("Id"),
                Title = rdr.GetString("Title"),
                Author = rdr.GetString("Author"),
                Year = rdr.GetInt32("Year")
            });
        }
        return list;
    }

    public Book GetById(int id)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var cmd = new MySqlCommand("SELECT * FROM Books WHERE Id = @Id", con);
        cmd.Parameters.AddWithValue("@Id", id);
        using var rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            return new Book
            {
                Id = rdr.GetInt32("Id"),
                Title = rdr.GetString("Title"),
                Author = rdr.GetString("Author"),
                Year = rdr.GetInt32("Year")
            };
        }
        return null;
    }

    public void Insert(Book book)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var cmd = new MySqlCommand("INSERT INTO Books (Title, Author, Year) VALUES (@Title, @Author, @Year)", con);
        cmd.Parameters.AddWithValue("@Title", book.Title);
        cmd.Parameters.AddWithValue("@Author", book.Author);
        cmd.Parameters.AddWithValue("@Year", book.Year);
        cmd.ExecuteNonQuery();
    }

    public void Update(Book book)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var cmd = new MySqlCommand("UPDATE Books SET Title=@Title, Author=@Author, Year=@Year WHERE Id=@Id", con);
        cmd.Parameters.AddWithValue("@Title", book.Title);
        cmd.Parameters.AddWithValue("@Author", book.Author);
        cmd.Parameters.AddWithValue("@Year", book.Year);
        cmd.Parameters.AddWithValue("@Id", book.Id);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var cmd = new MySqlCommand("DELETE FROM Books WHERE Id=@Id", con);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }
}
