using System;

public class StringFormator
{
    public string GetMethodName(Action action)
    {
        return action.Method.Name;
    }
}
