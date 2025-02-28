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

namespace Openchain.Ledger.Validation
{
    public class PermissionSet
    {
        public static PermissionSet AllowAll { get; } = new PermissionSet(Access.Permit, Access.Permit, Access.Permit, Access.Permit);

        public static PermissionSet DenyAll { get; } = new PermissionSet(Access.Deny, Access.Deny, Access.Deny, Access.Deny);

        public static PermissionSet Unset { get; } = new PermissionSet(Access.Unset, Access.Unset, Access.Unset, Access.Unset);

        public PermissionSet(
            Access accountNegative = Access.Unset,
            Access accountSpend = Access.Unset,
            Access accountModify = Access.Unset,
            Access dataModify = Access.Unset)
        {
            this.AccountNegative = accountNegative;
            this.AccountSpend = accountSpend;
            this.AccountModify = accountModify;
            this.DataModify = dataModify;
        }

        public Access AccountNegative { get; }

        public Access AccountSpend { get; }

        public Access AccountModify { get; }

        public Access DataModify { get; }

        public PermissionSet Add(PermissionSet added)
        {
            return new PermissionSet(
                accountNegative: Or(AccountNegative, added.AccountNegative),
                accountSpend: Or(AccountSpend, added.AccountSpend),
                accountModify: Or(AccountModify, added.AccountModify),
                dataModify: Or(DataModify, added.DataModify));
        }

        private static Access Or(Access left, Access right)
        {
            if (left == Access.Deny || right == Access.Deny)
                return Access.Deny;
            else if (left == Access.Permit || right == Access.Permit)
                return Access.Permit;
            else
                return Access.Unset;
        }

        public PermissionSet AddLevel(PermissionSet lowerLevel)
        {
            return new PermissionSet(
                accountNegative: lowerLevel.AccountNegative == Access.Unset ? AccountNegative : lowerLevel.AccountNegative,
                accountSpend: lowerLevel.AccountSpend == Access.Unset ? AccountSpend : lowerLevel.AccountSpend,
                accountModify: lowerLevel.AccountModify == Access.Unset ? AccountModify : lowerLevel.AccountModify,
                dataModify: lowerLevel.DataModify == Access.Unset ? DataModify : lowerLevel.DataModify);
        }
    }
}
