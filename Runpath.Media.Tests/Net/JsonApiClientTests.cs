using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Shouldly;
using Xunit;

namespace Runpath.Media.Net
{
    public class JsonApiClientTests
    {
        public const string ApiTestPath = nameof(JsonApiClientTests);

        private static IList<int> CreateIntegerSet()
        {
            return Builder<int>.CreateListOfSize(5).Build();
        }

        [Fact]
        public void Ctor_WithNullHttpClient_ShouldThrowException()
        {
            // ACT / ASSERT
            Assert.Throws<ArgumentNullException>(
                () => new JsonApiClient(null));
        }

        [Fact]
        public async void Get_WithEmptyArray_ShouldSucceedAndReturnEmptySet()
        {
            // ARRANGE
            var httpClient = HttpClientStub.Create(c =>
            {
                c.ForPath(ApiTestPath)
                 .ReturnJsonFor(new object[] { });
            });
            var sut = new JsonApiClient(httpClient);

            // ACT
            var result = await sut.Get<IEnumerable<int>>(ApiTestPath);

            // ASSERT
            result.ShouldBeEmpty();
        }

        [Fact]
        public async void Get_WithIntegerArray_ShouldSucceedAndReturnIntegerArray()
        {
            // ARRANGE
            var integerSet = CreateIntegerSet();
            var httpClient = HttpClientStub.Create(c =>
            {
                c.ForPath(ApiTestPath)
                 .ReturnJsonFor(integerSet);
            });
            var sut = new JsonApiClient(httpClient);

            // ACT
            var result = await sut.Get<IEnumerable<int>>(ApiTestPath);

            // ASSERT
            result.ShouldBe(integerSet);
        }
    }
}