using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Сигнатура метода, обрабатывающего удаление указанной сущности
    /// </summary>
    /// <param name="entity">Удаляемая сущность</param>
    public delegate void EntityRemovingHandler(Entity entity);

    public interface IEntitySet: IEnumerable
    {
        event EntityRemovingHandler EntityRemoving;
    }
}
