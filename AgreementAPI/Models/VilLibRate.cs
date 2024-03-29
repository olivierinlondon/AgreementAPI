﻿using System;
using System.Xml;
using System.Net;
using System.IO;


namespace AgreementAPI.Models
{
    public class VilLibRate
    {

        public static double GetCurrentRate(string baseCodeRate)
        {
            double currentRate = 0;

            var _url = "http://www.lb.lt/webservices/VilibidVilibor/VilibidVilibor.asmx";
            var _action = "http://webservices.lb.lt/VilibidVilibor/getLatestVilibRate";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(baseCodeRate);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(soapResult);
            XmlNodeList elemList = xml.GetElementsByTagName("getLatestVilibRateResult");

            double newRate=0;
            if (elemList.Count > 0)
            {
                if (Double.TryParse(elemList.Item(0).InnerXml, out newRate))
                    currentRate = newRate;
            }

            return currentRate;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string baseCodeRate)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();

            soapEnvelopeDocument.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">  
             <soap:Body>  
                <getLatestVilibRate xmlns=""http://webservices.lb.lt/VilibidVilibor"">
                    <RateType>"+ baseCodeRate + @"</RateType>
                 </getLatestVilibRate>
              </soap:Body>  
            </soap:Envelope>");

            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}