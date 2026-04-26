using System;
using static BaseState;

public interface ICellState
{
    event EventHandler<OnTargetFoundArgs> OnTargetFound;
    event EventHandler OnTargetLost;
    event EventHandler OnTargetEat;
}