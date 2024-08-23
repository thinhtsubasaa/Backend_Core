using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace ERP.Helpers
{
    public class MyTypedClient
    {
        public HttpClient Client { get; set; }
        public MyTypedClient(HttpClient client)
        {
            string apiKey = "THACO2017";
            client.BaseAddress = new Uri("https://portalgroupapi.thacochulai.vn");
            client.DefaultRequestHeaders.Add("Authorization", "APIKEY " + apiKey);
            client.DefaultRequestHeaders.Add("APIKEY", apiKey);
            this.Client = client;
        }

        public string AnhNhanVien(string MaNhanVien)
        {
            string url = "";
            var response = this.Client.GetAsync("/api/KeySecure/AnhNhanVien?MaNhanVien=" + MaNhanVien).Result;
            if (response.IsSuccessStatusCode)
            {
                url = response.Content.ReadAsStringAsync().Result;
            }
            return url;
        }
        public class NhanVien1
        {
            public string maNhanVien { get; set; }
            public string tenNhanVien { get; set; }
            public string hinhAnh_Url { get; set; }
            public string tenPhongBan { get; set; }
            public string chucDanh { get; set; }
            public string chucVu { get; set; }
            public string trangThai { get; set; }
        }
        public class NhanVienHRMModel
        {
            public string MaNhanVien { get; set; }
            public string TenNhanVien { get; set; }
            public string HinhAnh_Url { get; set; }
            public string TenPhongBan { get; set; }
        }
        public NhanVienHRMModel ThongTinNhanVien(string MaNhanVien)
        {
            var data = new NhanVienHRMModel();
            var response = this.Client.GetAsync("/api/KeySecure/ThongTinNhanVien?MaNhanVien=" + MaNhanVien).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<NhanVienHRMModel>(res);
            }
            return data;
        }


    }
    public class VehicleInfo
    {
        public string Id { get; set; }
        public string Plate { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        // Thêm các thuộc tính khác nếu cần
    }
    public class ApiResponse
    {
        public List<VehicleInfo> Data { get; set; }
    }

    public class VehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VehicleInfo>> GetVehicleInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://shareapi.adsun.vn/Vehicle/GpsInfos?pageIds=4279&username=cty4279&pwd=123456");
                response.EnsureSuccessStatusCode();

                var res = await response.Content.ReadAsStringAsync();
                var vehicleList = JsonConvert.DeserializeObject<ApiResponse>(res)?.Data;
                return vehicleList ?? new List<VehicleInfo>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new List<VehicleInfo>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return new List<VehicleInfo>();
            }
        }

        public async Task<VehicleInfo> GetVehicleByPlateAsync(string plate)
        {
            var vehicleList = await GetVehicleInfoAsync();
            var vehicle = vehicleList.FirstOrDefault(v => v.Plate.Equals(plate, StringComparison.OrdinalIgnoreCase));
            return vehicle;
        }
    }


}
