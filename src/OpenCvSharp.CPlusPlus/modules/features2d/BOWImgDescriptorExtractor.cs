﻿using System;

namespace OpenCvSharp.CPlusPlus
{
    // ReSharper disable once InconsistentNaming

    /// <summary>
    /// Brute-force descriptor matcher.
    /// For each descriptor in the first set, this matcher finds the closest descriptor in the second set by trying each one.
    /// </summary>
    public class BOWImgDescriptorExtractor : DisposableCvObject
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="dextractor">Descriptor extractor that is used to compute descriptors for an input image and its keypoints.</param>
        /// <param name="dmatcher">Descriptor matcher that is used to find the nearest word of the trained vocabulary for each keypoint descriptor of the image.</param>
        public BOWImgDescriptorExtractor(Feature2D dextractor, DescriptorMatcher dmatcher)
        {
            if (dextractor == null)
                throw new ArgumentNullException(nameof(dextractor));
            if (dmatcher == null)
                throw new ArgumentNullException(nameof(dmatcher));
            ptr = NativeMethods.features2d_BOWImgDescriptorExtractor_new1(
                dextractor.PtrObj, dmatcher.PtrObj);
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
        /// Releases the resources
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
#endif
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                try
                {
                    // releases managed resources
                    if (disposing)
                    {
                    }
                    else
                    {
                        if (ptr != IntPtr.Zero)
                            NativeMethods.features2d_BOWImgDescriptorExtractor_delete(ptr);
                        ptr = IntPtr.Zero;
                    }
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        /// <summary>
        /// Sets a visual vocabulary.
        /// </summary>
        /// <param name="vocabulary">Vocabulary (can be trained using the inheritor of BOWTrainer ). 
        /// Each row of the vocabulary is a visual word(cluster center).</param>
        public void SetVocabulary(Mat vocabulary)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            if (vocabulary == null)
                throw new ArgumentNullException(nameof(vocabulary));
            NativeMethods.features2d_BOWImgDescriptorExtractor_setVocabulary(ptr, vocabulary.CvPtr);
            GC.KeepAlive(this);
            GC.KeepAlive(vocabulary);
        }

        /// <summary>
        /// Returns the set vocabulary.
        /// </summary>
        /// <returns></returns>
        public Mat GetVocabulary()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            IntPtr p = NativeMethods.features2d_BOWImgDescriptorExtractor_getVocabulary(ptr);
            GC.KeepAlive(this);
            return new Mat(p);
        }

        /// <summary>
        /// Computes an image descriptor using the set visual vocabulary.
        /// </summary>
        /// <param name="image">Image, for which the descriptor is computed.</param>
        /// <param name="keypoints">Keypoints detected in the input image.</param>
        /// <param name="imgDescriptor">Computed output image descriptor.</param>
        /// <param name="pointIdxsOfClusters">pointIdxsOfClusters Indices of keypoints that belong to the cluster. 
        /// This means that pointIdxsOfClusters[i] are keypoint indices that belong to the i -th cluster(word of vocabulary) returned if it is non-zero.</param>
        /// <param name="descriptors">Descriptors of the image keypoints that are returned if they are non-zero.</param>
        public void Compute(Mat image, out KeyPoint[] keypoints, Mat imgDescriptor,
            out int[][] pointIdxsOfClusters, Mat descriptors = null)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (imgDescriptor == null)
                throw new ArgumentNullException(nameof(imgDescriptor));

            using (var keypointsVec = new VectorOfKeyPoint())
            using (var pointIdxsOfClustersVec = new VectorOfVectorInt())
            {
                NativeMethods.features2d_BOWImgDescriptorExtractor_compute1(ptr, image.CvPtr, keypointsVec.CvPtr,
                    imgDescriptor.CvPtr, pointIdxsOfClustersVec.CvPtr, Cv2.ToPtr(descriptors));
                GC.KeepAlive(this);
                keypoints = keypointsVec.ToArray();
                pointIdxsOfClusters = pointIdxsOfClustersVec.ToArray();
            }
            GC.KeepAlive(image);
            GC.KeepAlive(imgDescriptor);
            GC.KeepAlive(descriptors);
        }

        /// <summary>
        /// Computes an image descriptor using the set visual vocabulary.
        /// </summary>
        /// <param name="image">Image, for which the descriptor is computed.</param>
        /// <param name="keypoints">Keypoints detected in the input image.</param>
        /// <param name="imgDescriptor">Computed output image descriptor.</param>
        public void Compute2(Mat image, out KeyPoint[] keypoints, Mat imgDescriptor)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (imgDescriptor == null)
                throw new ArgumentNullException(nameof(imgDescriptor));

            using (var keypointsVec = new VectorOfKeyPoint())
            {
                NativeMethods.features2d_BOWImgDescriptorExtractor_compute2(
                    ptr, image.CvPtr, keypointsVec.CvPtr, imgDescriptor.CvPtr);
                GC.KeepAlive(this);
                keypoints = keypointsVec.ToArray();
            }
            GC.KeepAlive(image);
            GC.KeepAlive(imgDescriptor);
        }

        /// <summary>
        /// Returns an image descriptor size if the vocabulary is set. Otherwise, it returns 0.
        /// </summary>
        /// <returns></returns>
        public int DescriptorSize()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            var ret = NativeMethods.features2d_BOWImgDescriptorExtractor_descriptorSize(ptr);
            GC.KeepAlive(this);
            return ret;
        }

        /// <summary>
        /// Returns an image descriptor type.
        /// </summary>
        /// <returns></returns>
        public int DescriptorType()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            var ret = NativeMethods.features2d_BOWImgDescriptorExtractor_descriptorType(ptr);
            GC.KeepAlive(this);
            return ret;
        }
    }
}
