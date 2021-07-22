using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace csharp.controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("consum")]
        public string consum()
        {

            try
            {
                var url = "https://apiecommerce.redpagos.co:8530/CheckOut/MethodGenerateTransaction";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        generateTransaction = new
                        {
                            security = new
                            {
                                accountId = 30336,
                                token = "bflMObSMSnyvysHpC72*",
                            },
                            infoPayment = new
                            {
                                amount = 1000,
                                tax = 0,
                                description = "prueba c#",
                                toolId = 5,
                                registryToolId = 0,
                                currency = "cop",
                            },
                            infoClient = new
                            {
                                name = "Pagos Inteligentes",
                                idType = "CC",
                                idNumber = "123456789",
                                email = "comprobantes@pagosinteligentes.com",
                                phone = "573213285290",
                            },
                            infoAdditional = new
                            {
                                disabledPaymentMethod = "20,21,24",
                                infoAdditional = 0,
                                urlResponseOk = "https://sag.pagosinteligentes.com:8140/",
                                urlResponseFail = "https://sag.pagosinteligentes.com:8140/",
                                urlResponsePending = "https://sag.pagosinteligentes.com:8140/",
                                urlNotificationPost = "https://sag.pagosinteligentes.com:8140/",
                                photo = "https://dl.dropboxusercontent.com/s/jghrtm678do5fts/carrito.jpg?dl=0",
                                cashDiscount = 100,
                                expiredCashDiscount = "2021/12/31",
                                deliveryAddres = true,
                                ammountShipping = 0
                            },
                        }
                    }
                    );
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                try
                {
                    using WebResponse response = request.GetResponse();
                    using Stream strReader = response.GetResponseStream();
                    if (strReader == null)
                    {
                        return null;
                    }
                    using StreamReader objReader = new StreamReader(strReader);
                    string responseBody = objReader.ReadToEnd();
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
