﻿using NUnit.Framework;
using Tests.Helpers;
using Tests.Helpers.TokenHelpers;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Services.Authentication
{
	[TestFixture]
	public class TokenTest : BaseIntegrationTest
	{
		[Test]
		public async Task TokenAuthorizeTest()
		{
			var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await Client.GetAsync("api/users/getall");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

		}

		[Test]
		public async Task TokenExpiredTest()
		{
			var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			Thread.Sleep(10000);

			var response = await Client.GetAsync("api/users/getall");
			Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

		}

	}
}
