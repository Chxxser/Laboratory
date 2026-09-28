using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Интерфейс, который обязывает все сущности иметь уникальный идентификатор
    /// </summary>
    public interface IDomainObject
    {
        int Id { get; set; }
    }
}
