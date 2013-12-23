// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Common;
using Microsoft.WindowsAzure.Common.Internals;
using Microsoft.WindowsAzure.Management.VirtualNetworks;
using Microsoft.WindowsAzure.Management.VirtualNetworks.Models;

namespace Microsoft.WindowsAzure.Management.VirtualNetworks
{
    internal partial class ClientRootCertificateOperations : IServiceOperations<VirtualNetworkManagementClient>, IClientRootCertificateOperations
    {
        /// <summary>
        /// Initializes a new instance of the ClientRootCertificateOperations
        /// class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        internal ClientRootCertificateOperations(VirtualNetworkManagementClient client)
        {
            this._client = client;
        }
        
        private VirtualNetworkManagementClient _client;
        
        /// <summary>
        /// Gets a reference to the
        /// Microsoft.WindowsAzure.Management.VirtualNetworks.VirtualNetworkManagementClient.
        /// </summary>
        public VirtualNetworkManagementClient Client
        {
            get { return this._client; }
        }
        
        /// <summary>
        /// The Upload Client Root Certificate operation is used to upload a
        /// new client root certificate to Windows Azure.  (see
        /// http://msdn.microsoft.com/en-us/library/windowsazure/dn205129.aspx
        /// for more information)
        /// </summary>
        /// <param name='virtualNetworkName'>
        /// The name of the virtual network for this gateway.
        /// </param>
        /// <param name='parameters'>
        /// Parameters supplied to the Upload client certificate Virtual
        /// Network Gateway operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A standard storage response including an HTTP status code and
        /// request ID.
        /// </returns>
        public async Task<GatewayOperationResponse> CreateAsync(string virtualNetworkName, ClientRootCertificateCreateParameters parameters, CancellationToken cancellationToken)
        {
            // Validate
            if (virtualNetworkName == null)
            {
                throw new ArgumentNullException("virtualNetworkName");
            }
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            if (parameters.Certificate == null)
            {
                throw new ArgumentNullException("parameters.Certificate");
            }
            
            // Tracing
            bool shouldTrace = CloudContext.Configuration.Tracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = Tracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("virtualNetworkName", virtualNetworkName);
                tracingParameters.Add("parameters", parameters);
                Tracing.Enter(invocationId, this, "CreateAsync", tracingParameters);
            }
            
            // Construct URL
            string url = this.Client.BaseUri + "/" + this.Client.Credentials.SubscriptionId + "/services/networking/" + virtualNetworkName + "/gateway/clientrootcertificates";
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = null;
            try
            {
                httpRequest = new HttpRequestMessage();
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = new Uri(url);
                
                // Set Headers
                httpRequest.Headers.Add("x-ms-version", "2013-11-01");
                
                // Set Credentials
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                
                // Serialize Request
                string requestContent = null;
                requestContent = parameters.Certificate;
                httpRequest.Content = new StringContent(requestContent, Encoding.UTF8);
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                
                // Send Request
                HttpResponseMessage httpResponse = null;
                try
                {
                    if (shouldTrace)
                    {
                        Tracing.SendRequest(invocationId, httpRequest);
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                    httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                    if (shouldTrace)
                    {
                        Tracing.ReceiveResponse(invocationId, httpResponse);
                    }
                    HttpStatusCode statusCode = httpResponse.StatusCode;
                    if (statusCode != HttpStatusCode.Accepted)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        CloudException ex = CloudException.Create(httpRequest, requestContent, httpResponse, await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false), CloudExceptionType.Xml);
                        if (shouldTrace)
                        {
                            Tracing.Error(invocationId, ex);
                        }
                        throw ex;
                    }
                    
                    // Create Result
                    GatewayOperationResponse result = null;
                    // Deserialize Response
                    cancellationToken.ThrowIfCancellationRequested();
                    string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = new GatewayOperationResponse();
                    XDocument responseDoc = XDocument.Parse(responseContent);
                    
                    XElement gatewayOperationAsyncResponseElement = responseDoc.Element(XName.Get("GatewayOperationAsyncResponse", "http://schemas.microsoft.com/windowsazure"));
                    if (gatewayOperationAsyncResponseElement != null)
                    {
                        XElement idElement = gatewayOperationAsyncResponseElement.Element(XName.Get("ID", "http://schemas.microsoft.com/windowsazure"));
                        if (idElement != null)
                        {
                            string idInstance = idElement.Value;
                            result.OperationId = idInstance;
                        }
                    }
                    
                    result.StatusCode = statusCode;
                    if (httpResponse.Headers.Contains("x-ms-request-id"))
                    {
                        result.RequestId = httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
                    }
                    
                    if (shouldTrace)
                    {
                        Tracing.Exit(invocationId, result);
                    }
                    return result;
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
            finally
            {
                if (httpRequest != null)
                {
                    httpRequest.Dispose();
                }
            }
        }
        
        /// <summary>
        /// The Delete Client Root Certificate operation deletes a previously
        /// uploaded client root certificate. from Windows Azure  (see
        /// http://msdn.microsoft.com/en-us/library/windowsazure/dn205128.aspx
        /// for more information)
        /// </summary>
        /// <param name='virtualNetworkName'>
        /// The name of the virtual network for this gateway.
        /// </param>
        /// <param name='certificateThumbprint'>
        /// The X509 certificate thumbprint.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A standard storage response including an HTTP status code and
        /// request ID.
        /// </returns>
        public async Task<GatewayOperationResponse> DeleteAsync(string virtualNetworkName, string certificateThumbprint, CancellationToken cancellationToken)
        {
            // Validate
            if (virtualNetworkName == null)
            {
                throw new ArgumentNullException("virtualNetworkName");
            }
            if (certificateThumbprint == null)
            {
                throw new ArgumentNullException("certificateThumbprint");
            }
            
            // Tracing
            bool shouldTrace = CloudContext.Configuration.Tracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = Tracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("virtualNetworkName", virtualNetworkName);
                tracingParameters.Add("certificateThumbprint", certificateThumbprint);
                Tracing.Enter(invocationId, this, "DeleteAsync", tracingParameters);
            }
            
            // Construct URL
            string url = this.Client.BaseUri + "/" + this.Client.Credentials.SubscriptionId + "/services/networking/" + virtualNetworkName + "/gateway/clientrootcertificates/" + certificateThumbprint;
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = null;
            try
            {
                httpRequest = new HttpRequestMessage();
                httpRequest.Method = HttpMethod.Delete;
                httpRequest.RequestUri = new Uri(url);
                
                // Set Headers
                httpRequest.Headers.Add("x-ms-version", "2013-11-01");
                
                // Set Credentials
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                
                // Send Request
                HttpResponseMessage httpResponse = null;
                try
                {
                    if (shouldTrace)
                    {
                        Tracing.SendRequest(invocationId, httpRequest);
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                    httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                    if (shouldTrace)
                    {
                        Tracing.ReceiveResponse(invocationId, httpResponse);
                    }
                    HttpStatusCode statusCode = httpResponse.StatusCode;
                    if (statusCode != HttpStatusCode.OK)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        CloudException ex = CloudException.Create(httpRequest, null, httpResponse, await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false), CloudExceptionType.Xml);
                        if (shouldTrace)
                        {
                            Tracing.Error(invocationId, ex);
                        }
                        throw ex;
                    }
                    
                    // Create Result
                    GatewayOperationResponse result = null;
                    // Deserialize Response
                    cancellationToken.ThrowIfCancellationRequested();
                    string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = new GatewayOperationResponse();
                    XDocument responseDoc = XDocument.Parse(responseContent);
                    
                    XElement gatewayOperationAsyncResponseElement = responseDoc.Element(XName.Get("GatewayOperationAsyncResponse", "http://schemas.microsoft.com/windowsazure"));
                    if (gatewayOperationAsyncResponseElement != null)
                    {
                        XElement idElement = gatewayOperationAsyncResponseElement.Element(XName.Get("ID", "http://schemas.microsoft.com/windowsazure"));
                        if (idElement != null)
                        {
                            string idInstance = idElement.Value;
                            result.OperationId = idInstance;
                        }
                    }
                    
                    result.StatusCode = statusCode;
                    if (httpResponse.Headers.Contains("x-ms-request-id"))
                    {
                        result.RequestId = httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
                    }
                    
                    if (shouldTrace)
                    {
                        Tracing.Exit(invocationId, result);
                    }
                    return result;
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
            finally
            {
                if (httpRequest != null)
                {
                    httpRequest.Dispose();
                }
            }
        }
        
        /// <summary>
        /// The Get Client Root Certificate operation returns the public
        /// portion of a previously uploaded client root certificate in a
        /// base-64 encoded format from Windows Azure.  (see
        /// http://msdn.microsoft.com/en-us/library/windowsazure/dn205127.aspx
        /// for more information)
        /// </summary>
        /// <param name='virtualNetworkName'>
        /// The name of the virtual network for this gateway.
        /// </param>
        /// <param name='certificateThumbprint'>
        /// The X509 certificate thumbprint.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A standard storage response including an HTTP status code and
        /// request ID.
        /// </returns>
        public async Task<ClientRootCertificateGetResponse> GetAsync(string virtualNetworkName, string certificateThumbprint, CancellationToken cancellationToken)
        {
            // Validate
            if (virtualNetworkName == null)
            {
                throw new ArgumentNullException("virtualNetworkName");
            }
            if (certificateThumbprint == null)
            {
                throw new ArgumentNullException("certificateThumbprint");
            }
            
            // Tracing
            bool shouldTrace = CloudContext.Configuration.Tracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = Tracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("virtualNetworkName", virtualNetworkName);
                tracingParameters.Add("certificateThumbprint", certificateThumbprint);
                Tracing.Enter(invocationId, this, "GetAsync", tracingParameters);
            }
            
            // Construct URL
            string url = this.Client.BaseUri + "/" + this.Client.Credentials.SubscriptionId + "/services/networking/" + virtualNetworkName + "/gateway/clientrootcertificates/" + certificateThumbprint;
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = null;
            try
            {
                httpRequest = new HttpRequestMessage();
                httpRequest.Method = HttpMethod.Get;
                httpRequest.RequestUri = new Uri(url);
                
                // Set Headers
                httpRequest.Headers.Add("x-ms-version", "2013-11-01");
                
                // Set Credentials
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                
                // Send Request
                HttpResponseMessage httpResponse = null;
                try
                {
                    if (shouldTrace)
                    {
                        Tracing.SendRequest(invocationId, httpRequest);
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                    httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                    if (shouldTrace)
                    {
                        Tracing.ReceiveResponse(invocationId, httpResponse);
                    }
                    HttpStatusCode statusCode = httpResponse.StatusCode;
                    if (statusCode != HttpStatusCode.OK)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        CloudException ex = CloudException.Create(httpRequest, null, httpResponse, await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false), CloudExceptionType.Xml);
                        if (shouldTrace)
                        {
                            Tracing.Error(invocationId, ex);
                        }
                        throw ex;
                    }
                    
                    // Create Result
                    ClientRootCertificateGetResponse result = null;
                    // Deserialize Response
                    cancellationToken.ThrowIfCancellationRequested();
                    string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = new ClientRootCertificateGetResponse();
                    result.Certificate = responseContent;
                    
                    result.StatusCode = statusCode;
                    if (httpResponse.Headers.Contains("x-ms-request-id"))
                    {
                        result.RequestId = httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
                    }
                    
                    if (shouldTrace)
                    {
                        Tracing.Exit(invocationId, result);
                    }
                    return result;
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
            finally
            {
                if (httpRequest != null)
                {
                    httpRequest.Dispose();
                }
            }
        }
        
        /// <summary>
        /// The List Client Root Certificates operation returns a list of all
        /// the client root certificates that are associated with the
        /// specified virtual network in Windows Azure.  (see
        /// http://msdn.microsoft.com/en-us/library/windowsazure/dn205130.aspx
        /// for more information)
        /// </summary>
        /// <param name='virtualNetworkName'>
        /// The name of the virtual network for this gateway.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The response to the list client root certificates request.
        /// </returns>
        public async Task<ClientRootCertificateListResponse> ListAsync(string virtualNetworkName, CancellationToken cancellationToken)
        {
            // Validate
            if (virtualNetworkName == null)
            {
                throw new ArgumentNullException("virtualNetworkName");
            }
            
            // Tracing
            bool shouldTrace = CloudContext.Configuration.Tracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = Tracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("virtualNetworkName", virtualNetworkName);
                Tracing.Enter(invocationId, this, "ListAsync", tracingParameters);
            }
            
            // Construct URL
            string url = this.Client.BaseUri + "/" + this.Client.Credentials.SubscriptionId + "/services/networking/" + virtualNetworkName + "/gateway/clientrootcertificates";
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = null;
            try
            {
                httpRequest = new HttpRequestMessage();
                httpRequest.Method = HttpMethod.Get;
                httpRequest.RequestUri = new Uri(url);
                
                // Set Headers
                httpRequest.Headers.Add("x-ms-version", "2013-11-01");
                
                // Set Credentials
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                
                // Send Request
                HttpResponseMessage httpResponse = null;
                try
                {
                    if (shouldTrace)
                    {
                        Tracing.SendRequest(invocationId, httpRequest);
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                    httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                    if (shouldTrace)
                    {
                        Tracing.ReceiveResponse(invocationId, httpResponse);
                    }
                    HttpStatusCode statusCode = httpResponse.StatusCode;
                    if (statusCode != HttpStatusCode.OK)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        CloudException ex = CloudException.Create(httpRequest, null, httpResponse, await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false), CloudExceptionType.Xml);
                        if (shouldTrace)
                        {
                            Tracing.Error(invocationId, ex);
                        }
                        throw ex;
                    }
                    
                    // Create Result
                    ClientRootCertificateListResponse result = null;
                    // Deserialize Response
                    cancellationToken.ThrowIfCancellationRequested();
                    string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = new ClientRootCertificateListResponse();
                    XDocument responseDoc = XDocument.Parse(responseContent);
                    
                    XElement clientRootCertificatesSequenceElement = responseDoc.Element(XName.Get("ClientRootCertificates", "http://schemas.microsoft.com/windowsazure"));
                    if (clientRootCertificatesSequenceElement != null)
                    {
                        foreach (XElement clientRootCertificatesElement in clientRootCertificatesSequenceElement.Elements(XName.Get("ClientRootCertificate", "http://schemas.microsoft.com/windowsazure")))
                        {
                            ClientRootCertificateListResponse.ClientRootCertificate clientRootCertificateInstance = new ClientRootCertificateListResponse.ClientRootCertificate();
                            result.ClientRootCertificates.Add(clientRootCertificateInstance);
                            
                            XElement expirationTimeElement = clientRootCertificatesElement.Element(XName.Get("ExpirationTime", "http://schemas.microsoft.com/windowsazure"));
                            if (expirationTimeElement != null)
                            {
                                DateTime expirationTimeInstance = DateTime.Parse(expirationTimeElement.Value, CultureInfo.InvariantCulture);
                                clientRootCertificateInstance.ExpirationTime = expirationTimeInstance;
                            }
                            
                            XElement subjectElement = clientRootCertificatesElement.Element(XName.Get("Subject", "http://schemas.microsoft.com/windowsazure"));
                            if (subjectElement != null)
                            {
                                string subjectInstance = subjectElement.Value;
                                clientRootCertificateInstance.Subject = subjectInstance;
                            }
                            
                            XElement thumbprintElement = clientRootCertificatesElement.Element(XName.Get("Thumbprint", "http://schemas.microsoft.com/windowsazure"));
                            if (thumbprintElement != null)
                            {
                                string thumbprintInstance = thumbprintElement.Value;
                                clientRootCertificateInstance.Thumbprint = thumbprintInstance;
                            }
                        }
                    }
                    
                    result.StatusCode = statusCode;
                    if (httpResponse.Headers.Contains("x-ms-request-id"))
                    {
                        result.RequestId = httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
                    }
                    
                    if (shouldTrace)
                    {
                        Tracing.Exit(invocationId, result);
                    }
                    return result;
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
            finally
            {
                if (httpRequest != null)
                {
                    httpRequest.Dispose();
                }
            }
        }
    }
}
