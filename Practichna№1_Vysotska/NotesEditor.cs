using System.Collections.Generic;

public class NotesEditor
{
    private Stack<string> undoStack =
        new Stack<string>();

    private Stack<string> redoStack =
        new Stack<string>();

    private string currentText = "";

    public void Write(string text)
    {
        undoStack.Push(currentText);

        currentText = text;

        redoStack.Clear();
    }

    public void Append(string text)
    {
        undoStack.Push(currentText);

        currentText += text;

        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(currentText);

            currentText = undoStack.Pop();
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(currentText);

            currentText = redoStack.Pop();
        }
    }

    public void Clear()
    {
        undoStack.Push(currentText);

        currentText = "";
    }

    public string GetText()
    {
        return currentText;
    }
}