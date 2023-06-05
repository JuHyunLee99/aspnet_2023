using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TodoItemApp.Models
{
    public class TodoItemsCollection: ObservableCollection<TodoItem>
    // ObservableCollection
    //  ListView와 함께 사용하여  동적인 목록을 표시하고 변경 사항을 실시간으로 반영할 수 있습니다.                                  
    //  해당 변경 사항을 감지하고 처리할 수 있음.
    //  OnCollectionChanged: 컬렉션 변경 사항을 발생시키기 위해 사용                   
    // IList<TodoItem>, List<TodoItem>
    {
        public void CopyFrom(IEnumerable<TodoItem> items)
        {
            this.Items.Clear(); // ObservableCollection<T> 자체가 Items 속성을 가지고 있음 모두 삭제

            foreach (TodoItem item in items)
            {
                this.Items.Add(item);   // 하나씩 다시 추가
            }
            // 데이터바꼈어요(전부 초기화)!  컬렉션 변경 사항을 발생
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
