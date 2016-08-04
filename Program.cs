using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WSAClient.WSAWebService;

namespace WSAClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = "Y";
            Console.WriteLine();
            while (opt == "Y" || opt == "y")
            {
                //e.g. Test cases:
                //     10443720012345678901    => email   is "mauricio.solorzano@streamenergy.net"
                //     1100515222-07706855D    => email   is empty
                //     1111                    => PO does NOT exist
                var purchaseOrderNumber = "";
                Console.Write("Give me Purchase Order Number : ");
                purchaseOrderNumber = Console.ReadLine();

                PurchaseOrderSoapClient service = new PurchaseOrderSoapClient();

                string result;

                var purchaseOrderInfoRequest = new PurchaseOrderInfoRequest()
                {
                    Authentication = new clsAuthentication()
                    {
                        UserName = "APITestLYRIC",
                        Password = "WSA@cc3ss!"
                    },
                    PurchaseOrderNumber = purchaseOrderNumber
                };

                try
                {
                    var getPurchaseOrderResponse = service.GetPurchaseOrder(purchaseOrderInfoRequest);


                    //GetPurchaseOrderResponse: WSAClient.WSAWebService.clsPurchaseOrder
                    if (getPurchaseOrderResponse == null || getPurchaseOrderResponse.PurchaseOrder.email == null
                        || String.IsNullOrEmpty(getPurchaseOrderResponse.PurchaseOrder.email)
                    )
                    {
                        result = String.Format("PO Number: \"{0}\", \nE-Mail: \"\"", purchaseOrderNumber);
                    }
                    else
                    {
                        result = String.Format("PO Number: \"{0}\", \nE-Mail: \"{1}\",", 
                                purchaseOrderNumber, getPurchaseOrderResponse.PurchaseOrder.email);
                    }
                }
                catch (Exception)
                {

                    result = String.Format("PO Number: \"{0}\" does NOT exist",   purchaseOrderNumber);
                }

                Console.WriteLine(result);
                Console.Write("\nDo you want to get another PO? (Y/N) => ");
                opt = Console.ReadLine();
                Console.WriteLine();
            }
        }
    }
}
