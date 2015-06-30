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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Management.RemoteApp.Model;

namespace Microsoft.Azure.Management.RemoteApp
{
    /// <summary>
    /// RemoteApp collection operations.
    /// </summary>
    public partial interface ICollectionOperations
    {
        /// <summary>
        /// Gets the collection details.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group
        /// </param>
        /// <param name='collectionName'>
        /// The automation account name.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The response for the get collection operation.
        /// </returns>
        Task<GetCollectionOperationResult> GetAsync(string resourceGroupName, string collectionName, CancellationToken cancellationToken);
        
        /// <summary>
        /// Gets the list of collections details in the resource group.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The response for the list collection operation.
        /// </returns>
        Task<ListCollectionOperationResult> ListAsync(string resourceGroupName, CancellationToken cancellationToken);
    }
}
