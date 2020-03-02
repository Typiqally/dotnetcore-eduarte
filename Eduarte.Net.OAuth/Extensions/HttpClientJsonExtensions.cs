﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eduarte.Net.OAuth.Extensions
{
	public static class HttpClientJsonExtensions
	{
		public static async Task<JsonHttpResponseMessage<T>> ExecuteRequestAsync<T>(
			this HttpClient httpClient,
			HttpRequestMessage requestMessage)
		{
			var response = await httpClient
				.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead)
				.ConfigureAwait(false);

			await using var contentStream = await response.Content.ReadAsStreamAsync();
			var data = await JsonSerializer.DeserializeAsync<T>(contentStream);

			return new JsonHttpResponseMessage<T>(response, data);
		}

		public static async Task<JsonHttpResponseMessage<T>> ExecuteRequestAsync<T>(
			this HttpClient httpClient,
			string uri = null)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, uri ?? "/");
			return await httpClient.ExecuteRequestAsync<T>(request);
		}
		
		public static async Task<JsonHttpResponseMessage<T>> ExecuteRequestAsync<T>(
			this OAuthHttpClient httpClient,
			HttpRequestMessage requestMessage)
		{
			var response = await httpClient
				.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead)
				.ConfigureAwait(false);

			await using var contentStream = await response.Content.ReadAsStreamAsync();
			var data = await JsonSerializer.DeserializeAsync<T>(contentStream);

			return new JsonHttpResponseMessage<T>(response, data);
		}

		public static async Task<JsonHttpResponseMessage<T>> ExecuteRequestAsync<T>(
			this OAuthHttpClient httpClient,
			string uri = null)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, uri ?? "/");
			return await httpClient.ExecuteRequestAsync<T>(request);
		}
	}
}