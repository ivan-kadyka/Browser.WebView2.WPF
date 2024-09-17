using Reactive.Extensions;
using Reactive.Extensions.Observable;


using System.Collections.Generic;

public class UndoRedoStack<T>
{
    // Gets the current value
    public IObservableValue<T> Current => _current;

    // Checks if there are actions to undo
    public bool CanUndo => undoStack.Count > 0;

    // Checks if there are actions to redo
    public bool CanRedo => redoStack.Count > 0;
    
    private readonly Stack<T> undoStack = new Stack<T>();
    private readonly Stack<T> redoStack = new Stack<T>();

    private ObservableValue<T> _current;

    public UndoRedoStack(T initialValue)
    {
        _current = new ObservableValue<T>(initialValue);
    }


    // Perform an action and save it to the undo stack
    public void Do(T newValue)
    {
        var prevValue = _current.Value;
        undoStack.Push(prevValue);
        redoStack.Clear();
        
        _current.OnNext(newValue);
    }

    // Undo the last action
    public void Undo()
    {
        if (CanUndo)
        {
            var undoValue = _current.Value;
            redoStack.Push(undoValue);
            
            var currentVaue = undoStack.Pop();
            _current.OnNext(currentVaue);
        }
    }

    // Redo the last undone action
    public void Redo()
    {
        if (CanRedo)
        {
            var redoValue = _current.Value;
            undoStack.Push(redoValue);
            
            var current = redoStack.Pop();
            _current.OnNext(current);
        }
    }
}
