<<<<<<< HEAD
using System.Collections;

namespace Dialog.Logical_Lines{

    public interface ILogical_Lines{
        
        string keyword { get; }
        bool Matches(Dialog_Line line);
        IEnumerator Execute(Dialog_Line line);

    }

}
=======
using System.Collections;

namespace Dialog.Logical_Lines{

    public interface ILogical_Lines{
        
        string keyword { get; }
        bool Matches(Dialog_Line line);
        IEnumerator Execute(Dialog_Line line);

    }

}
>>>>>>> c8d4f422 (Whole Template)
