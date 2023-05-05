﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressMarket.Services
{

    internal class UserDataTables
    {
        public UserDataTables()
        {

            DataTable UserTable = new();
            DataTable AccessTable = new();

            DataColumn User_ID = new DataColumn("User_ID", System.Type.GetType("System.Int16"));
            DataColumn UserName = new DataColumn("UserName", System.Type.GetType("System.String"));
            DataColumn UserPassword = new DataColumn("UserPassword", System.Type.GetType("System.String"));
            DataColumn AccessType = new DataColumn("UserAccessType", System.Type.GetType("System.Int16"));

            DataColumn Access_ID = new DataColumn("Access_ID", System.Type.GetType("System.Int16"));
            DataColumn AccessName = new DataColumn("AccessName", System.Type.GetType("System.String"));
            DataColumn CreateObject = new DataColumn("CreateObject", System.Type.GetType("System.Boolean"));
            DataColumn DestroyObject = new DataColumn("DestroyObject", System.Type.GetType("System.Boolean"));
            DataColumn ModifyObject = new DataColumn("ModifyObject", System.Type.GetType("System.Boolean"));
            DataColumn ChangeUserRights = new DataColumn("ChangeUserRights", System.Type.GetType("System.Boolean"));

            //UserTable
            UserTable.TableName = "Users";
            User_ID.AutoIncrement = true;
            User_ID.Unique = true;
            UserTable.Columns.Add(User_ID);

            UserName.Unique = true;
            UserTable.Columns.Add(UserName);
            UserTable.Columns.Add(UserPassword);
            UserTable.Columns.Add(AccessType);


            //AccessTable
            AccessTable.TableName = "Access";

            Access_ID.AutoIncrement = true;
            Access_ID.Unique = true;
            AccessTable.Columns.Add(Access_ID);

            AccessName.Unique = true;
            AccessTable.Columns.Add(AccessName);

            AccessTable.Columns.Add(CreateObject);
            AccessTable.Columns.Add(DestroyObject);
            AccessTable.Columns.Add(ModifyObject);
            AccessTable.Columns.Add(ChangeUserRights);

            GlobalsTools.UserSet.Tables.Add(UserTable);
            GlobalsTools.UserSet.Tables.Add(AccessTable);

            DataColumn parentColumn = GlobalsTools.UserSet.Tables["Access"].Columns["Access_ID"];
            DataColumn childColumn = GlobalsTools.UserSet.Tables["Users"].Columns["UserAccessType"];

            DataRelation relation = new DataRelation("Access2User", parentColumn, childColumn);

            GlobalsTools.UserSet.Tables["Users"].ParentRelations.Add(relation);
        }
    }
    internal class UserManagementServices
    {
        public UserManagementServices()
        {}
    }
}

