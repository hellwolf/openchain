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

using Xunit;

namespace Openchain.Ledger.Tests
{
    public class SignatureEvidenceTests
    {
        private readonly ByteString payload = ByteString.Parse("b27982ff2a4fa0c163da8651ca1902e27b651e68a6d0a4f6bff4a78d7dcf718d");

        [Fact]
        public void VerifySignature_Valid()
        {
            SignatureEvidence signature = new SignatureEvidence(
                ByteString.Parse("0213b0006543d4ab6e79f49559fbfb18e9d73596d63f39e2f12ebc2c9d51e2eb06"),
                ByteString.Parse("304402200c7fba6b623efd7e52731a11e6d7b99c2ae752c0f950b7a444ef7fb80162498c02202b01c74a4a04fb120860494de09bd6848f088927a7b07e3c3925b3894c8c89d4"));

            Assert.True(signature.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidPublicKey()
        {
            SignatureEvidence signature = new SignatureEvidence(
                ByteString.Parse("0013b0006543d4ab6e79f49559fbfb18e9d73596d63f39e2f12ebc2c9d51e2eb06"),
                ByteString.Parse("304402200c7fba6b623efd7e52731a11e6d7b99c2ae752c0f950b7a444ef7fb80162498c02202b01c74a4a04fb120860494de09bd6848f088927a7b07e3c3925b3894c8c89d4"));

            Assert.False(signature.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidSignature()
        {
            SignatureEvidence signature = new SignatureEvidence(
                ByteString.Parse("0213b0006543d4ab6e79f49559fbfb18e9d73596d63f39e2f12ebc2c9d51e2eb06"),
                ByteString.Parse("304402200c7fba6b623efd7e52731a11e6d7b99c2ae752c0f950b7a444ef7fb80162498c02202b01c74a4a04fb120860494de09bd6848f088927a7b07e3c3925b3894c8c89d5"));

            Assert.False(signature.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidLengths()
        {
            SignatureEvidence signature = new SignatureEvidence(ByteString.Empty, ByteString.Empty);

            Assert.False(signature.VerifySignature(payload.ToByteArray()));
        }
    }
}
