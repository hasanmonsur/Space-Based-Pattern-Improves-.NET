using Hazelcast.DistributedObjects;
using Hazelcast;
using SpaceBasedPatternApi.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace SpaceBasedPatternApi.Services
{
    public class ProductService
    {
        private readonly IHazelcastClient _hazelcastClient;

        public ProductService(IHazelcastClient hazelcastClient)
        {
            _hazelcastClient = hazelcastClient;
        }

        public async Task<string> GetOrAddAsync(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }

            try
            {
                var map = await _hazelcastClient.GetMapAsync<string, string>("my-distributed-map");
                var existingValue = await map.GetAsync(key);
                if (existingValue != null)
                {
                    return existingValue; // Return the existing value
                }

                // If the key does not exist, add the new value
                await map.PutAsync(key, value);
                return value; // Return the new value
            }
            catch (Exception ex)
            {
                // Log exception and handle accordingly
                throw new InvalidOperationException("An error occurred while accessing the Hazelcast map.", ex);
            }
        }

        public async Task<string> GetAsync(string key)
        {
            try
            {
                var map = await _hazelcastClient.GetMapAsync<string, string>("my-distributed-map");
                return await map.GetAsync(key);
            }
            catch (Exception ex)
            {
                // Log exception and handle accordingly
                throw new InvalidOperationException("An error occurred while accessing the Hazelcast map.", ex);
            }
        }
    }
}
