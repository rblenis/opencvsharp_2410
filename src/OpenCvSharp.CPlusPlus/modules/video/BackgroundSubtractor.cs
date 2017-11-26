﻿using System;

namespace OpenCvSharp.CPlusPlus
{
    /// <summary>
    /// The Base Class for Background/Foreground Segmentation.
    /// The class is only used to define the common interface for
    /// the whole family of background/foreground segmentation algorithms.
    /// </summary>
    public class BackgroundSubtractor : Algorithm
    {
        /// <summary>
        /// cv::Ptr&lt;FeatureDetector&gt;
        /// </summary>
        private Ptr<BackgroundSubtractor> objectPtr;
        /// <summary>
        /// 
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Creates instance from cv::Ptr&lt;T&gt; .
        /// ptr is disposed when the wrapper disposes. 
        /// </summary>
        /// <param name="ptr"></param>
        internal static BackgroundSubtractor FromPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException("Invalid BackgroundSubtractor pointer");

            var ptrObj = new Ptr<BackgroundSubtractor>(ptr);
            var obj = new BackgroundSubtractor
            {
                objectPtr = ptrObj,
                ptr = ptrObj.Obj
            };
            return obj;
        }
        /// <summary>
        /// Creates instance from raw T*
        /// </summary>
        /// <param name="ptr"></param>
        internal static BackgroundSubtractor FromRawPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException("Invalid BackgroundSubtractor pointer");
            var obj = new BackgroundSubtractor
            {
                objectPtr = null,
                ptr = ptr
            };
            return obj;
        }

        /// <summary>
        /// Releases the resources
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    // releases managed resources
                    if (disposing)
                    {
                    }
                    // releases unmanaged resources
                    if (IsEnabledDispose)
                    {
                        if (objectPtr != null)
                            objectPtr.Dispose();
                        objectPtr = null;
                        ptr = IntPtr.Zero;
                    }
                    disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        /// <summary>
        /// Pointer to algorithm information (cv::AlgorithmInfo*)
        /// </summary>
        /// <returns></returns>
        public override IntPtr InfoPtr
        {
            get
            {
                var ret = NativeMethods.video_BackgroundSubtractor_info(ptr);
                GC.KeepAlive(this);
                return ret;
            }
        }

        /// <summary>
        /// the update operator that takes the next video frame and returns the current foreground mask as 8-bit binary image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fgmask"></param>
        /// <param name="learningRate"></param>
        public virtual void Run(InputArray image, OutputArray fgmask, double learningRate = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (fgmask == null)
                throw new ArgumentNullException(nameof(fgmask));
            image.ThrowIfDisposed();
            fgmask.ThrowIfNotReady();
            NativeMethods.video_BackgroundSubtractor_operator(ptr, image.CvPtr, fgmask.CvPtr, learningRate);
            GC.KeepAlive(this);
            GC.KeepAlive(image);
            fgmask.Fix();
        }

        /// <summary>
        /// computes a background image
        /// </summary>
        /// <param name="backgroundImage"></param>
        public virtual void GetBackgroundImage(OutputArray backgroundImage)
        {
            if (backgroundImage == null)
                throw new ArgumentNullException(nameof(backgroundImage));
            backgroundImage.ThrowIfNotReady();
            NativeMethods.video_BackgroundSubtractor_getBackgroundImage(ptr, backgroundImage.CvPtr);
            GC.KeepAlive(this);
            backgroundImage.Fix();
        }
    }
}
