using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Contoso.Storage.Table {
    class Program {
        static void Main(string[] args) {

            CloudTableClient tableClient = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("roster");
            table.CreateIfNotExists();

            Employee first = new Employee {
                PartitionKey = "IT", RowKey = "ibahena",
                YearsAtCompany = 7
            };
            Employee second = new Employee {
                PartitionKey = "HR", RowKey = "rreeves",
                YearsAtCompany = 12
            };
            Employee third = new Employee {
                PartitionKey = "HR", RowKey = "rromani",
                YearsAtCompany = 3
            };

            TableOperation insertOperation = TableOperation.InsertOrReplace(first);
            table.Execute(insertOperation);


            // Batch operations can be used to insert multiple entities into an Azure Storage table.
            // The entities must all have the same PartitionKey in order to be inserted as a single batch.
            // Batch operations are atomic
            TableBatchOperation batchOperation = new TableBatchOperation();
            batchOperation.InsertOrReplace(second);
            batchOperation.InsertOrReplace(third);
            table.ExecuteBatch(batchOperation);

            TableQuery<Employee> query = new TableQuery<Employee>().Where( TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "HR"));
            Console.WriteLine("HR Employees\n");
            foreach (Employee hrEmployee in table.ExecuteQuery<Employee>(query)) {
                Console.WriteLine(hrEmployee);
            }

            Console.WriteLine("\n\n\n\nIT Employee\n");
            TableOperation retrieveOperation = TableOperation.Retrieve<Employee>("IT", "ibahena");
            TableResult result = table.Execute(retrieveOperation);
            Employee itEmployee = result.Result as Employee;
            Console.WriteLine(itEmployee);

            Console.ReadLine();
        }
    }
}
