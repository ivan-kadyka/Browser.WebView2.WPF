using Reactive.Extensions.Observable;

namespace Browser.App.Tests.Stubs;

using System;
using System.Collections.Generic;

internal class UndoRedoStack<T>
{
    private readonly Stack<T> _undoStack = new();
    private readonly Stack<T> _redoStack = new();

    public IObservableValue<T> Current => _current;
    
    private readonly ObservableValue<T> _current;
    

    public UndoRedoStack(T initialValue)
    {
        _current = new ObservableValue<T>(initialValue);
    }
    
    public bool CanUndo => _undoStack.Count > 0;
    
    public bool CanRedo => _redoStack.Count > 0;
    
    public void Do(T newValue)
    {
        _undoStack.Push(_current.Value);
        _redoStack.Clear();
        _current.OnNext(newValue);
      
    }
    
    public void Undo()
    {
        if (CanUndo)
        {
            _redoStack.Push(_current.Value);
            var currentValue = _undoStack.Pop();
            _current.OnNext(currentValue);
        }
        else
        {
            throw new InvalidOperationException("No actions to undo.");
        }
    }
    
    public void Redo()
    {
        if (CanRedo)
        {
            var current = _redoStack.Pop();
            _undoStack.Push(current);
            _current.OnNext(current);
        }
        else
        {
            throw new InvalidOperationException("No actions to redo.");
        }
    }
}