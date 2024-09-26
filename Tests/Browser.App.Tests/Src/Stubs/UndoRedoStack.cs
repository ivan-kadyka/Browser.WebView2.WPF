using Reactive.Extensions.Observable;

namespace Browser.App.Tests.Stubs;

using System;
using System.Collections.Generic;

internal class UndoRedoStack<T>
{
    private readonly Stack<T> undoStack = new Stack<T>();
    private readonly Stack<T> redoStack = new Stack<T>();

    public IObservableValue<T> Current => _current;
    
    private readonly ObservableValue<T> _current;
    

    public UndoRedoStack(T initialValue)
    {
        _current = new ObservableValue<T>(initialValue);
    }



    // Checks if there are actions to undo
    public bool CanUndo => undoStack.Count > 0;

    // Checks if there are actions to redo
    public bool CanRedo => redoStack.Count > 0;

    // Perform an action and save it to the undo stack
    public void Do(T newValue)
    {
        undoStack.Push(_current.Value);
        _current.OnNext(newValue);
        redoStack.Clear();  // Clear redo stack since we have a new action
    }

    // Undo the last action
    public void Undo()
    {
        if (CanUndo)
        {
            redoStack.Push(_current.Value);
            var currentValue = undoStack.Pop();
            _current.OnNext(currentValue);
        }
        else
        {
            throw new InvalidOperationException("No actions to undo.");
        }
    }

    // Redo the last undone action
    public void Redo()
    {
        if (CanRedo)
        {
            var current = undoStack.Pop();
            undoStack.Push(current);
            _current.OnNext(redoStack.Pop());
        }
        else
        {
            throw new InvalidOperationException("No actions to redo.");
        }
    }
}