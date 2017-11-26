﻿using System;

namespace OpenCvSharp.CPlusPlus.Gpu
{
#if LANG_JP
    /// <summary>
    /// ページロックされたメモリ割り当てによる制約付きの cv::Mat
    /// </summary>
#else
    /// <summary>
    /// CudaMem is limited cv::Mat with page locked memory allocation.
    /// </summary>
#endif
    public sealed class CudaMem : DisposableGpuObject, ICloneable
    {
        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool disposed;

        #region Init and Disposal

#if LANG_JP
    /// <summary>
    /// OpenCVネイティブの cv::gpu::CudaMem* ポインタから初期化
    /// </summary>
    /// <param name="ptr"></param>
#else
        /// <summary>
        /// Creates from native cv::gpu::CudaMem* pointer
        /// </summary>
        /// <param name="ptr"></param>
#endif
        public CudaMem(IntPtr ptr)
        {
            ThrowIfNotAvailable();
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException("Native object address is NULL");
            this.ptr = ptr;
        }

        /// <summary>
        /// Creates empty CudaMem
        /// </summary>
        public CudaMem()
        {
            ThrowIfNotAvailable();
            ptr = NativeMethods.gpu_CudaMem_new1();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        public CudaMem(CudaMem m)
        {
            ThrowIfNotAvailable();
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            ptr = NativeMethods.gpu_CudaMem_new2(m.CvPtr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows">Number of rows in a 2D array.</param>
        /// <param name="cols">Number of columns in a 2D array.</param>
        /// <param name="type">Array type. </param>
        /// <param name="allocType"></param>
        public CudaMem(int rows, int cols, MatType type, 
            CudaMemAllocType allocType = CudaMemAllocType.Locked)
        {
            ThrowIfNotAvailable();
            ptr = NativeMethods.gpu_CudaMem_new3(rows, cols, type, (int)allocType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">2D array size: Size(cols, rows) </param>
        /// <param name="type">Array type. </param>
        /// <param name="allocType"></param>
        public CudaMem(Size size, MatType type, 
            CudaMemAllocType allocType = CudaMemAllocType.Locked)
            : this(size.Height, size.Width, type, allocType)
        {            
        }

#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
#else
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
#endif
        public void Release()
        {
            Dispose(true);
        }
#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
        /// <param name="disposing">
        /// trueの場合は、このメソッドがユーザコードから直接が呼ばれたことを示す。マネージ・アンマネージ双方のリソースが解放される。
        /// falseの場合は、このメソッドはランタイムからファイナライザによって呼ばれ、もうほかのオブジェクトから参照されていないことを示す。アンマネージリソースのみ解放される。
        ///</param>
#else
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
#endif
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    if (disposing)
                    {                        
                    }
                    if (IsEnabledDispose)
                    {
                        NativeMethods.gpu_CudaMem_delete(ptr);
                    }
                    disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        #endregion

        #region Cast
        /// <summary>
        /// returns matrix header with disabled reference counting for CudaMem data.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static explicit operator Mat(CudaMem self)
        {
            if (self == null)
                return null;
            return self.CreateMatHeader();
        }

        /// <summary>
        /// maps host memory into device address space and returns GpuMat header for it. 
        /// Throws exception if not supported by hardware.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static implicit operator GpuMat(CudaMem self)
        {
            if (self == null)
                return null;
            return self.CreateGpuMatHeader();
        }
        #endregion

        #region Properties

        /// <summary>
        /// includes several bit-fields: 
        ///  1.the magic signature 
        ///  2.continuity flag 
        ///  3.depth 
        ///  4.number of channels
        /// </summary>
        public int Flags
        {
            get
            {
                ThrowIfDisposed();
                var ret = NativeMethods.gpu_CudaMem_flags(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// the number of rows
        /// </summary>
        public int Rows
        {
            get 
            {
                ThrowIfDisposed();
                var ret = NativeMethods.gpu_CudaMem_rows(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// the number of columns
        /// </summary>
        public int Cols
        {
            get
            {
                ThrowIfDisposed();
                var ret = NativeMethods.gpu_CudaMem_cols(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// the number of rows
        /// </summary>
        public int Height
        {
            get
            {
                ThrowIfDisposed();
                var ret = NativeMethods.gpu_CudaMem_rows(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// the number of columns
        /// </summary>
        public int Width
        {
            get
            {
                ThrowIfDisposed();
                var ret = NativeMethods.gpu_CudaMem_cols(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// pointer to the data
        /// </summary>
        public unsafe IntPtr Data
        {
            get
            {
                ThrowIfDisposed();
                var ret = (IntPtr)NativeMethods.gpu_CudaMem_data(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// pointer to the reference counter;
        /// when matrix points to user-allocated data, the pointer is NULL
        /// </summary>
        public unsafe IntPtr RefCount
        {
            get
            {
                ThrowIfDisposed();
                var ret = (IntPtr)NativeMethods.gpu_CudaMem_refcount(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// helper fields used in locateROI and adjustROI
        /// </summary>
        public unsafe IntPtr DataStart
        {
            get
            {
                ThrowIfDisposed();
                var ret = (IntPtr)NativeMethods.gpu_CudaMem_datastart(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// helper fields used in locateROI and adjustROI
        /// </summary>
        public unsafe IntPtr DataEnd
        {
            get
            {
                ThrowIfDisposed();
                var ret = (IntPtr)NativeMethods.gpu_CudaMem_dataend(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CudaMemAllocType AllocType
        {
            get
            {
                ThrowIfDisposed();
                var ret = (CudaMemAllocType)NativeMethods.gpu_CudaMem_alloc_type(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }
        #endregion

        #region Public methods
        
        /// <summary>
        /// returns true iff the GpuMatrix data is continuous
        /// (i.e. when there are no gaps between successive rows).
        /// similar to CV_IS_GpuMat_CONT(cvGpuMat->type)
        /// </summary>
        /// <returns></returns>
        public bool IsContinuous()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_isContinuous(ptr) != 0;
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns the number of matrix channels.
        /// </summary>
        /// <returns></returns>
        public int Channels()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_channels(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns the depth of a matrix element.
        /// </summary>
        /// <returns></returns>
        public int Depth()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_depth(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns the matrix element size in bytes.
        /// </summary>
        /// <returns></returns>
        public long ElemSize()
        {
            ThrowIfDisposed();
            var ret = (long)NativeMethods.gpu_CudaMem_elemSize(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns the size of each matrix element channel in bytes.
        /// </summary>
        /// <returns></returns>
        public long ElemSize1()
        {
            ThrowIfDisposed();
            var ret = (long)NativeMethods.gpu_CudaMem_elemSize1(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns a matrix size.
        /// </summary>
        /// <returns></returns>
        public Size Size()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_size(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// a distance between successive rows in bytes; includes the gap if any
        /// </summary>
        public long Step()
        {
            ThrowIfDisposed();
            var ret = (long)NativeMethods.gpu_CudaMem_step(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns a normalized step.
        /// </summary>
        public long Step1()
        {
            ThrowIfDisposed();
            var ret = (long)NativeMethods.gpu_CudaMem_step1(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns the type of a matrix element.
        /// </summary>
        /// <returns></returns>
        public MatType Type()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_type(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// returns true if GpuMatrix data is NULL
        /// </summary>
        /// <returns></returns>
        public bool Empty()
        {
            ThrowIfDisposed();
            var ret = NativeMethods.gpu_CudaMem_empty(ptr) != 0;
            GC.KeepAlive(this);
        }

        /// <summary>
        /// returns deep copy of the matrix, i.e. the data is copied
        /// </summary>
        /// <returns></returns>
        public CudaMem Clone()
        {
            ThrowIfDisposed();
            IntPtr ret = NativeMethods.gpu_CudaMem_clone(ptr);
            GC.KeepAlive(this);
            return new CudaMem(ret);
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void AssignTo(CudaMem target)
        {
            ThrowIfDisposed();
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            NativeMethods.gpu_CudaMem_opAssign(target.CvPtr, ptr);
            GC.KeepAlive(this);
            GC.KeepAlive(target);
        }

        /// <summary>
        /// allocates new matrix data unless the matrix already has specified size and type.
        /// </summary>
        /// <param name="rows">Number of rows in a 2D array.</param>
        /// <param name="cols">Number of columns in a 2D array.</param>
        /// <param name="type">Array type. </param>
        /// <param name="allocType"></param>
        public void Create(
            int rows, int cols, MatType type, 
            CudaMemAllocType allocType = CudaMemAllocType.Locked)
        {
            ThrowIfDisposed();
            NativeMethods.gpu_CudaMem_create(ptr, rows, cols, type, (int)allocType);
            GC.KeepAlive(this);
        }

        /// <summary>
        /// returns matrix header with disabled reference counting for CudaMem data.
        /// </summary>
        /// <returns></returns>
        public Mat CreateMatHeader()
        {
            ThrowIfDisposed();
            IntPtr ret = NativeMethods.gpu_CudaMem_createMatHeader(ptr);
            GC.KeepAlive(this);
            return new Mat(ret);
        }

        /// <summary>
        /// maps host memory into device address space and returns GpuMat header for it. Throws exception if not supported by hardware.
        /// </summary>
        /// <returns></returns>
        public GpuMat CreateGpuMatHeader()
        {
            ThrowIfDisposed();
            IntPtr ret = NativeMethods.gpu_CudaMem_createGpuMatHeader(ptr);
            GC.KeepAlive(this);
            return new GpuMat(ret);
        }

        /// <summary>
        /// returns if host memory can be mapperd to gpu address space;
        /// </summary>
        /// <returns></returns>
        public static bool CanMapHostMemory()
        {
            return NativeMethods.gpu_CudaMem_canMapHostMemory() != 0;
        }
        #endregion
    }
}


