﻿using System;
using FluentDispatch.Models;

namespace FluentDispatch.Nodes.Remote.Dual
{
    /// <summary>
    /// Node which process items remotely.
    /// </summary>
    /// <typeparam name="TInput1"></typeparam>
    /// <typeparam name="TInput2"></typeparam>
    internal interface IDualDispatcherRemoteNode<TInput1, TInput2> : INode, IDisposable
    {
        /// <summary>
        /// Dispatch a <see cref="TInput1"/> to the node.
        /// </summary>
        /// <param name="item1"><see cref="LinkedItem{TInput1}"/> to broadcast</param>
        void Dispatch(LinkedItem<TInput1> item1);

        /// <summary>
        /// Dispatch a <see cref="TInput2"/> to the node.
        /// </summary>
        /// <param name="item2"><see cref="LinkedItem{TInput2}"/> to broadcast</param>
        void Dispatch(LinkedItem<TInput2> item2);

        /// <summary>
        /// Dispatch a <see cref="TInput1"/> to the node.
        /// </summary>
        /// <param name="item1"><see cref="Func{TResult}"/> to broadcast</param>
        void Dispatch(LinkedFuncItem<TInput1> item1);

        /// <summary>
        /// Dispatch a <see cref="TInput2"/> to the node.
        /// </summary>
        /// <param name="item2"><see cref="Func{TResult}"/> to broadcast</param>
        void Dispatch(LinkedFuncItem<TInput2> item2);
    }
}