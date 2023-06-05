﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoItemApp.Models;

namespace TodoItemApp
{
    public class DivCode
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private List<DivCode> divCodes = new List<DivCode>();
        HttpClient client = new HttpClient();
        TodoItemsCollection todoItems = new TodoItemsCollection();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            divCodes.Add(new DivCode { Key = "True", Value = "1" });
            divCodes.Add(new DivCode { Key = "False", Value = "0" });
            CboIsComplete.ItemsSource = divCodes;
            CboIsComplete.DisplayMemberPath = "Key";    // 콤보박스에 True/False 추가
            // yyyy-MM-dd HH:mm:ss 날짜 포맷(오전/오후 포함)
            DtpTodoDate.Culture = new CultureInfo("ko-KR");

            // RestApi 호출 시작    // 아래 두 문장은 다시 호출하면 안됨
            client.BaseAddress = new System.Uri("https://localhost:7058/"); // RestAPI 서버 기본 URL
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            GetData();  // API 데이터 로드 메서드 호출


        }

        // RestAPI Get Method 호출
        private async void GetData()
        {
            GrdTodoItems.ItemsSource = todoItems;   // 미리 바인딩

            try // API 호출
            {
                // https://localhost:7058/api/TodoItems
                HttpResponseMessage? response = await client.GetAsync("api/TodoItems");  //Get method 비동기 호출
                response.EnsureSuccessStatusCode(); // 에러가 났으면 오류코드를 던진다(예외발생)

                // 응답에서 List<TodoItem> 형식으로 읽어옴
                var items = await response.Content.ReadAsAsync<IEnumerable<TodoItem>>();
                todoItems.CopyFrom(items);
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                await this.ShowMessageAsync("error", jEx.Message, MessageDialogStyle.Affirmative, new MetroDialogSettings()
                {
                    // 메시지 다이얼로그가 나타날 때와 사라질 때 애니메이션 효과
                    AnimateShow = true,
                    AnimateHide = true
                });
            }
            catch (HttpRequestException ex)
            {
                await this.ShowMessageAsync("error", ex.Message, MessageDialogStyle.Affirmative, new MetroDialogSettings()
                {
                    AnimateShow = true,
                    AnimateHide = true
                });
            }
        }
        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var todoItem = new TodoItem()
                {
                    Id = 0,
                    Title = TxtTitle.Text,
                    TodoDate = ((DateTime)DtpTodoDate.SelectedDateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    IsComplete = Int32.Parse((CboIsComplete.SelectedItem as DivCode).Value)
                };

                // Insert하라때는 POST메서드 사용
                var response = await client.PostAsJsonAsync("api/TodoItems", todoItem);
                response.EnsureSuccessStatusCode();

                GetData();

                TxtId.Text = TxtTitle.Text = string.Empty;
                CboIsComplete.SelectedIndex = -1;
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                await this.ShowMessageAsync("error", jEx.Message, MessageDialogStyle.Affirmative, new MetroDialogSettings()
                {
                    // 메시지 다이얼로그가 나타날 때와 사라질 때 애니메이션 효과
                    AnimateShow = true,
                    AnimateHide = true
                });
            }
            catch (HttpRequestException ex)
            {
                await this.ShowMessageAsync("error", ex.Message, MessageDialogStyle.Affirmative, new MetroDialogSettings()
                {
                    AnimateShow = true,
                    AnimateHide = true
                });
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
