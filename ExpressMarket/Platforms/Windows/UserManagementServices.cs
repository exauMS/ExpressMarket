using System.Data.Common;
using System.Data.OleDb;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ExpressMarket.Services;

public partial class UserManagementServices
{
    public OleDbDataAdapter UsersAdapter = new ();
    public OleDbConnection Connexion = new();

    public partial void ConfigTools()
    {
        //1 Definir le ConnectionString 
        Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
                                      + "Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "UserAccounts.accdb")
                                      + ";Persist Security Info=false";

        //2 Etablir les commandes prédéfinis
        string Insert_CommandText = "INSERT INTO DB_Users (UserName,UserPassword,UserAccessType) VALUES (@UserName,@UserPassword,@UserAccessType);";
        string Delete_CommandText = "DELETE FROM DB_Users WHERE UserName = @UserName;";
        string Select_CommandText = "SELECT * FROM DB_Users ORDER BY User_ID;";
        string Update_CommandText = "UPDATE DB_Users SET UserName = @UserName, UserPassword = @UserPassword, UserAccessType = @UserAccessType WHERE User_ID = @User_ID;";


        //3 Insertion des CommandText dans les commandes de OleDB
        OleDbCommand Insert_Command = new OleDbCommand(Insert_CommandText, Connexion);
        OleDbCommand Delete_Command = new OleDbCommand(Delete_CommandText, Connexion);
        OleDbCommand Select_Command = new OleDbCommand(Select_CommandText, Connexion);
        OleDbCommand Update_Command = new OleDbCommand(Update_CommandText, Connexion);


        //4 Liaison des commandes à l'adapter
        UsersAdapter.InsertCommand = Insert_Command;
        UsersAdapter.DeleteCommand = Delete_Command;
        UsersAdapter.SelectCommand = Select_Command;
        UsersAdapter.UpdateCommand = Update_Command;


        //5 Liaison aux tables définis
        UsersAdapter.TableMappings.Add("DB_Users", "Users");

        UsersAdapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UsersAdapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UsersAdapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        UsersAdapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UsersAdapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UsersAdapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UsersAdapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        UsersAdapter.UpdateCommand.Parameters.Add("@User_ID", OleDbType.Numeric, 100, "User_ID");

    }
    public async Task ReadAccessTable()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Access ORDER BY Access_ID;", Connexion);
        try
        {
            GlobalsTools.UserSet.Tables["Access"].Clear(); // Clear pour éviter le double clique error
            Connexion.Open();
            OleDbDataReader Reader = SelectCommand.ExecuteReader(); // on lis la commande SELECT executé 
            if (Reader.HasRows)
            {
                while (Reader.Read())// Lecture de chaques colonne de la table Access
                {
                    GlobalsTools.UserSet.Tables["Access"].Rows.Add(new object[] { Reader[0], Reader[1], Reader[2], Reader[3], Reader[4], Reader[5]});
                }
            }
            Reader.Close();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }



    public async Task ReadUserTable()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Users ORDER BY User_ID;", Connexion);
        try
        {
            
            Connexion.Open();
            OleDbDataReader Reader = SelectCommand.ExecuteReader();
            if (Reader.HasRows)
            {


                while (Reader.Read()) // Lecture de chaques colonne de la table Users
                {
                    GlobalsTools.UserSet.Tables["Users"].Rows.Add(new object[] { Reader[0], Reader[1], Reader[2], Reader[3]});
                }
            }
            Reader.Close();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Read database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }


    public async Task FillUserTable()
    {
        GlobalsTools.UserSet.Tables["Users"].Clear();
        try
        {
            Connexion.Open();
            UsersAdapter.Fill(GlobalsTools.UserSet.Tables["Users"]);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fill database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    public async Task InsertUser(string name, string password, Int16 access)
    {
        try
        {
            Connexion.Open();
            UsersAdapter.InsertCommand.Parameters[0].Value = name;
            UsersAdapter.InsertCommand.Parameters[1].Value = password;
            UsersAdapter.InsertCommand.Parameters[2].Value = access;
            if (UsersAdapter.InsertCommand.ExecuteNonQuery() != 0 )
            {
                await Shell.Current.DisplayAlert("Database", "User successfully inserted", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Database", "User not inserted", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Insert database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }
    
    public async Task DeleteUser(string name)
    {

        try
        {
            Connexion.Open();
            UsersAdapter.DeleteCommand.Parameters[0].Value = name;
       
            if (UsersAdapter.DeleteCommand.ExecuteNonQuery() != 0) await Shell.Current.DisplayAlert("Database", "User successfully deleted", "OK");
            else await Shell.Current.DisplayAlert("Database", "User not deleted", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    internal async Task UpdateUser(Int32 user_id, string name, string password, Int32 access)
    {
        UsersAdapter.UpdateCommand.Parameters[0].Value = name;
        UsersAdapter.UpdateCommand.Parameters[1].Value = password;
        UsersAdapter.UpdateCommand.Parameters[2].Value = access;
        UsersAdapter.UpdateCommand.Parameters[3].Value = user_id;
        try
        {
            Connexion.Open();

            UsersAdapter.UpdateCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }
}
