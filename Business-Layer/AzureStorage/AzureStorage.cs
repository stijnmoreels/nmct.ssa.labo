using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ssa.labo.webshop.businesslayer.AzureStorage
{
    public class AzureStorage
    {
        public CloudStorageAccount _account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public void BlobStorage(string containerName, string blobName, Stream stream)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(containerName);
            container.CreateIfNotExists();
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            blob.UploadFromStream(stream);
        }

        public void TableStorage(string tableName, TableEntity entity)
        {
            CloudTableClient client = _account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(tableName);
            table.CreateIfNotExists();
            table.Execute(TableOperation.Insert(entity));
        }

        public void QueueStorage(string queueName, string content)
        {
            CloudQueueClient client = _account.CreateCloudQueueClient();
            CloudQueue queue = client.GetQueueReference(queueName);
            queue.CreateIfNotExists();
            CloudQueueMessage message = new CloudQueueMessage(content);
            queue.AddMessage(message);
        }
    }
}
