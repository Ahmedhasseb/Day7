
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Data;
namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            SqlConnection conect = new SqlConnection(ConfigurationManager.ConnectionStrings["cli"].ToString());

            int id;
            string name;
            int phone;

            int num;
                int z;
            bool x;
            do
            {
                Console.Write("1-Insert Data  ");
                Console.Write("2-Show Data  ");
                Console.Write("3-Delet Data  ");
                Console.Write("4-Update Data  ");
                x = int.TryParse(Console.ReadLine(), out num);

                for (int i = 0; i < num; i++)
                {
                    if (num == 1)
                    {
                        Console.WriteLine("Enter Isert Data");
                        bool flag = true;
                        do
                        {
                            Console.Write("id : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Name : ");
                            name = Console.ReadLine();
                            Console.Write("Phone : ");
                            phone = int.Parse(Console.ReadLine());
                            InsertData(conect, id, name, phone);

                        } while (!flag);
                    }
                    if (num == 2)
                    {
                        ReadData(conect);
                        break;

                    }
                    if (num == 3)
                    {
                        Console.Write("Please Enter Your Id Delet : ");
                        bool flag = true;


                        id = int.Parse(Console.ReadLine());



                        Deletdata(conect, id);
                        flag = false;
                        break;


                    }
                    if (num == 4)
                    {

                        Console.Write("Update id : ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("update Name : ");
                        name = Console.ReadLine();
                        Console.Write("Update Phone : ");
                        phone = int.Parse(Console.ReadLine());
                        Update(conect, id, name, phone);
                        break;
                    }

                }

                Console.WriteLine("Do you want another modification?\n 1- Y  2- N");
                z = int.Parse(Console.ReadLine());



            } while ((z < 2 && x != false));
            ;
            //ReadData(conect);
            //Deletdata(conect, 6);
            //Update(conect, 5, "ehdhgfgf", 645454);
            //ReadData(conect);

            Console.ReadKey();
        }

        public static void InsertData(SqlConnection con, int id, string Name, int Mobile)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_AddStudent";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@mobile", Mobile);


            int rowsucces = cmd.ExecuteNonQuery();
            Console.WriteLine($"One Row {rowsucces}");
        }
        public static void ReadData(SqlConnection con)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student";

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                Console.WriteLine("No student");
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {((int)reader[0]).ToString()}, " +
                        $"  Name: {((string)reader[1]).ToString()}, " +
                        $"Mobile: {((int)reader[2]).ToString()}");
                }

                reader.NextResult();

              

            }
           

        }

        public static void Deletdata(SqlConnection con, int id)
        {


            string sql = $"Delete From Student Where Id='{id}'";
            SqlCommand command = new SqlCommand(sql, con);

            con.Open();
           int x= command.ExecuteNonQuery();
            con.Close();
            Console.WriteLine($"{x} row effect");
            con.Close();

        }
        public static void Update(SqlConnection con, int id,string name ,int number)
        {

            
                string sql = $"Update Student SET id='{id}', Name='{name}', Mobil='{number}' Where Id='{id}'";
            SqlCommand command = new SqlCommand(sql, con);
                
                    con.Open();
                 var x=  command.ExecuteNonQuery();
                    con.Close();
            Console.WriteLine($"{x} row effect");
                
            



        }
    }
}
