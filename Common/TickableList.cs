using System.Collections.Generic;

namespace QuaStateMachine
{
    public sealed class TickableList
    {
        public IFixedTickable FixedTickables => this.fixedTickables;

        public IPostFixedTickable PostFixedTickables => this.postFixedTickables;

        public ITickable Tickables => this.tickables;

        public IPostTickable PostTickables => this.postTickables;

        public ILateTickable LateTickables => this.lateTickables;

        public IPostLateTickable PostLateTickables => this.postLateTickables;

        private readonly IFixedTickableList fixedTickables;
        private readonly IPostFixedTickableList postFixedTickables;
        private readonly ITickableList tickables;
        private readonly IPostTickableList postTickables;
        private readonly ILateTickableList lateTickables;
        private readonly IPostLateTickableList postLateTickables;

        public TickableList()
        {
            this.fixedTickables = new IFixedTickableList();
            this.postFixedTickables = new IPostFixedTickableList();
            this.tickables = new ITickableList();
            this.postTickables = new IPostTickableList();
            this.lateTickables = new ILateTickableList();
            this.postLateTickables = new IPostLateTickableList();
        }

        public void Add<T>(T item)
        {
            this.fixedTickables.TryAdd(item);
            this.postFixedTickables.TryAdd(item);
            this.tickables.TryAdd(item);
            this.postTickables.TryAdd(item);
            this.lateTickables.TryAdd(item);
            this.postLateTickables.TryAdd(item);
        }

        private sealed class IFixedTickableList : List<IFixedTickable>, IFixedTickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is IFixedTickable t)
                    Add(t);
            }

            public void FixedTick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].FixedTick();
                }
            }
        }

        private sealed class IPostFixedTickableList : List<IPostFixedTickable>, IPostFixedTickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is IPostFixedTickable t)
                    Add(t);
            }

            public void PostFixedTick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].PostFixedTick();
                }
            }
        }

        private sealed class ITickableList : List<ITickable>, ITickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is ITickable t)
                    Add(t);
            }

            public void Tick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].Tick();
                }
            }
        }

        private sealed class IPostTickableList : List<IPostTickable>, IPostTickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is IPostTickable t)
                    Add(t);
            }

            public void PostTick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].PostTick();
                }
            }
        }

        private sealed class ILateTickableList : List<ILateTickable>, ILateTickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is ILateTickable t)
                    Add(t);
            }

            public void LateTick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].LateTick();
                }
            }
        }

        private sealed class IPostLateTickableList : List<IPostLateTickable>, IPostLateTickable
        {
            public void TryAdd<T>(T item)
            {
                if (item is IPostLateTickable t)
                    Add(t);
            }

            public void PostLateTick()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    this[i].PostLateTick();
                }
            }
        }
    }
}
