using Microsoft.Azure;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTable
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("connectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("users");

            TableBatchOperation batchOperation = new TableBatchOperation();

            Users users = new Users("username", "name");
            users.PhoneNumber = "mobile number";
            users.Email = "mail address";

            TableOperation insertOperation = TableOperation.Insert(users);
            table.Execute(insertOperation);

            Console.WriteLine("Inserted");
            Console.ReadKey();
        }
    }
    class Users : TableEntity
    {
        public Users(string UserName, string Name)
        {
            this.PartitionKey = UserName;
            this.RowKey = Name;
        }
        public Users() { }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
