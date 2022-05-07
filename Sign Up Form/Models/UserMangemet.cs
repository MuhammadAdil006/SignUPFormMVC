using System;
using Microsoft.Data.SqlClient;

namespace Sign_Up_Form.Models
{
    public class UserMangemet
    {
        SqlConnection con;
        String Connection;
        public static List<User> u;
        public UserMangemet()
        {
            Connection = @"Data Source=(localdb)\ProjectModels;Initial Catalog=User;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            u = new List<User>();
            con= new SqlConnection(Connection);
        }
        //checking login credentials
        public int CheckCredentials(User u)
        {
            u.email = u.email.ToLower();
            String query = "select id from Login where email=@a and password=@b";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("a", u.email);
            SqlParameter p2 = new SqlParameter("b", u.New_password);
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
            SqlDataReader dr = com.ExecuteReader();
            int id=-1;
            dr.Read();
            if(dr.HasRows)
            {
                id = Convert.ToInt32(dr[0]);
            }
            
            con.Close();
            return id;
        }
        //reverse scenario

        public void GetUsers()
        {
            u.Clear();
            //get a list of user from database
            String query = "select * from UserData ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader dr = com.ExecuteReader();
            while(dr.Read())
            {
                User user = new User();
                user.id = Convert.ToInt32(dr[0]);
                user.firstname =Convert.ToString( dr[1]);
                user.lastname =Convert.ToString(dr[2]);
                user.email =Convert.ToString(dr[4]); 
                user.gender =Convert.ToString( dr[5]);
                user.date=Convert.ToDateTime(dr[6]);
                u.Add(user);
            }
            con.Close();
           
           
        }
        //converting date into string
        public String MakeDate(User a)
        {
            String date = Convert.ToString(a.year);
            date = date + "-" + Convert.ToString(a.month) + "-" + Convert.ToString(a.day);
            if(date=="0-0-0")
            {
                date = "1-1-1950";
            }
            return date;
        }
        //getting id formed by db
        public int getIdFromDb(User a)
        {
            String query = "select id from UserData where email=@a and password=@b";
            con.Open();
            SqlCommand com= new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("a", a.email);
            SqlParameter p2 = new SqlParameter("b", a.New_password);
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            int id = Convert.ToInt32(dr[0]);
            con.Close();
            return id;
            

        }

        //adding credentionals to login table
        public void addLoginDetails(User a,int id)
        {
            //adding credentials
            String query = "insert into Login(id,email,password) values (@a,@b,@c)";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("a", id);
            SqlParameter p2 = new SqlParameter("b", a.email);
            SqlParameter p3 = new SqlParameter("c", a.New_password);
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
            com.Parameters.Add(p3);
            com.ExecuteNonQuery();
            
            con.Close();
        }
        //it will convert all credentials to lowercase except password so comparing doesnt matter
        public User ConvertoLowercase(User a)
        {
            a.email = a.email.ToLower();
            a.firstname = a.firstname.ToLower();
            a.lastname = a.lastname.ToLower();
            a.gender = a.gender.ToLower();
          
            return a;
        }
        public bool compareEmail(User a)
        {
            con.Open();
            String query = "select email from UserData where email=@a";
            SqlParameter p1 = new SqlParameter("a", a.email);
           
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(p1);

            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                con.Close();
                return true;
            }

            con.Close();
            return false;
        }
        public bool addUser(User a)
        {
            //check if email exists if exits then return false esle return true
            //add  to db
            //MAKING DATE

            //converting to lowercase
            a = ConvertoLowercase(a);
            if(!compareEmail(a))
            {
                
                con.Open();
                String query = "Insert into UserData(firstName,LastName,Password,Email,Gender,DateOfBirth) values (@a,@b,@c,@d,@e,@f)";
                SqlParameter p1 = new SqlParameter("a", a.firstname);
                SqlParameter p2 = new SqlParameter("b", a.lastname);
                SqlParameter p3 = new SqlParameter("c", a.New_password);
                SqlParameter p4 = new SqlParameter("d", a.email);
                SqlParameter p5 = new SqlParameter("e", a.gender);
                SqlParameter p6 = new SqlParameter("f", a.date);
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add(p1);
                com.Parameters.Add(p2);
                com.Parameters.Add(p3);
                com.Parameters.Add(p4);
                com.Parameters.Add(p6);
                
                com.Parameters.Add(p5);
                com.ExecuteNonQuery();
                con.Close();
                addLoginDetails(a, getIdFromDb(a));
                return true;
            }
            return false;
        }
    }
}
