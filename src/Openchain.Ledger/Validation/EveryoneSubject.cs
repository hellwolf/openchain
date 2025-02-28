﻿// Copyright 2015 Coinprism, Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;

namespace Openchain.Ledger.Validation
{
    /// <summary>
    /// Represents an implementation of the <see cref="IPermissionSubject"/> interface that is always matching.
    /// </summary>
    public class EveryoneSubject : IPermissionSubject
    {
        public static EveryoneSubject Instance { get; } = new EveryoneSubject();

        public bool IsMatch(IReadOnlyList<SignatureEvidence> authentication)
        {
            return true;
        }
    }
}
