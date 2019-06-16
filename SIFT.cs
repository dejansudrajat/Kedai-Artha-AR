using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using OpenCVMarkerLessAR;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using OpenCVForUnity;

namespace SIFT
{
    public class Texture2DMarkerAR : MonoBehaviour
    {
        /// <summary>
        /// The pattern texture.
        /// </summary>
        public Texture2D patternTexture;

        /// <summary>
        /// The pattern raw image.
        /// </summary>
        public RawImage patternRawImage;

        /// <summary>
        /// The image texture.
        /// </summary>
        public Texture2D imgTexture;

        /// <summary>
        /// The AR camera.
        /// </summary>
        public Camera ARCamera;

        /// <summary>
        /// Determines if should move AR camera.
        /// </summary>
        public bool shouldMoveARCamera;
        
        /// <summary>
        /// The AR game object.
        /// </summary>
        public GameObject ARGameObject;

        // Use this for initialization
        void Start ()
        {
            Mat patternMat = new Mat (patternTexture.height, patternTexture.width, CvType.CV_8UC4);
            
            Utils.texture2DToMat (patternTexture, patternMat);
            Debug.Log ("patternMat dst ToString " + patternMat.ToString ());

            patternRawImage.texture = patternTexture;
            patternRawImage.rectTransform.localScale = new Vector3 (1.0f, (float)patternMat.height () / (float)patternMat.width (), 1.0f);

        
            Mat imgMat = new Mat (imgTexture.height, imgTexture.width, CvType.CV_8UC4);
            Mat img2Mat = new Mat (imgTexture.height, imgTexture.width, CvType.CV_8UC3);
       
            Utils.texture2DToMat (imgTexture, imgMat);
            Debug.Log ("imgMat dst ToString " + imgMat.ToString ());

            Utils.texture2DToMat (imgTexture, img2Mat);
            Debug.Log ("img2Mat.ToString() " + img2Mat.ToString ());

            gameObject.transform.localScale = new Vector3 (imgTexture.width, imgTexture.height, 1);
            Debug.Log ("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);


            float width = imgMat.width ();
            float height = imgMat.height ();
            
            float imageSizeScale = 1.0f;
            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;
            if (widthScale < heightScale) {
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
                imageSizeScale = (float)Screen.height / (float)Screen.width;
            } else {
                Camera.main.orthographicSize = height / 2;
            }
            
            //set cameraparam
            int max_d = (int)Mathf.Max (width, height);
            double fx = max_d;
            double fy = max_d;
            double cx = width / 2.0f;
            double cy = height / 2.0f;
            Mat camMatrix = new Mat (3, 3, CvType.CV_64FC1);
            camMatrix.put (0, 0, fx);
            camMatrix.put (0, 1, 0);
            camMatrix.put (0, 2, cx);
            camMatrix.put (1, 0, 0);
            camMatrix.put (1, 1, fy);
            camMatrix.put (1, 2, cy);
            camMatrix.put (2, 0, 0);
            camMatrix.put (2, 1, 0);
            camMatrix.put (2, 2, 1.0f);
            Debug.Log ("camMatrix " + camMatrix.dump ());
            
            
            MatOfDouble distCoeffs = new MatOfDouble (0, 0, 0, 0);
            Debug.Log ("distCoeffs " + distCoeffs.dump ());
            
            
            //calibration camera
            Size imageSize = new Size (width * imageSizeScale, height * imageSizeScale);
            double apertureWidth = 0;
            double apertureHeight = 0;
            double[] fovx = new double[1];
            double[] fovy = new double[1];
            double[] focalLength = new double[1];
            Point principalPoint = new Point (0, 0);
            double[] aspectratio = new double[1];
            
            Calib3d.calibrationMatrixValues (camMatrix, imageSize, apertureWidth, apertureHeight, fovx, fovy, focalLength, principalPoint, aspectratio);
            
            Debug.Log ("imageSize " + imageSize.ToString ());
            Debug.Log ("apertureWidth " + apertureWidth);
            Debug.Log ("apertureHeight " + apertureHeight);
            Debug.Log ("fovx " + fovx [0]);
            Debug.Log ("fovy " + fovy [0]);
            Debug.Log ("focalLength " + focalLength [0]);
            Debug.Log ("principalPoint " + principalPoint.ToString ());
            Debug.Log ("aspectratio " + aspectratio [0]);
            
            
            //To convert the difference of the FOV value of the OpenCV and Unity. 
            double fovXScale = (2.0 * Mathf.Atan ((float)(imageSize.width / (2.0 * fx)))) / (Mathf.Atan2 ((float)cx, (float)fx) + Mathf.Atan2 ((float)(imageSize.width - cx), (float)fx));
            double fovYScale = (2.0 * Mathf.Atan ((float)(imageSize.height / (2.0 * fy)))) / (Mathf.Atan2 ((float)cy, (float)fy) + Mathf.Atan2 ((float)(imageSize.height - cy), (float)fy));
            
            Debug.Log ("fovXScale " + fovXScale);
            Debug.Log ("fovYScale " + fovYScale);
            
            
            //Adjust Unity Camera FOV https://github.com/opencv/opencv/commit/8ed1945ccd52501f5ab22bdec6aa1f91f1e2cfd4
            if (widthScale < heightScale) {
                ARCamera.fieldOfView = (float)(fovx [0] * fovXScale);
            } else {
                ARCamera.fieldOfView = (float)(fovy [0] * fovYScale);
            }
           

            float angle = UnityEngine.Random.Range (0, 360), scale = 1.0f;

            Point center = new Point (img2Mat.cols () * 0.5f, img2Mat.rows () * 0.5f);

            Mat affine_matrix = Imgproc.getRotationMatrix2D (center, angle, scale);

            Imgproc.warpAffine (img1Mat, img2Mat, affine_matrix, img2Mat.size ());

            /*
            ekstrasi image feature dengan algoritma SIFT dimana nilai dibawah merupakan nilai ketetapan
            yang sudah disarankan oleh penemu algoritma SIFT yaitu David. G. Lowe
            */

            int nfeatures = 0;
            int nOctaveLayers = 3;
            double contrastThreshold = 0.04;
            double edgeThreshold = 10;
            double sigma = 1.6;

            SIFT detector = SIFT.create (nfeatures, nOctaveLayers, contrastThreshold, edgeThreshold, sigma);

            DescriptorExtractor extractor = DescriptorExtractor.create (DescriptorExtractor.SIFT);

            MatOfKeyPoint keypoints1 = new MatOfKeyPoint ();
            Mat descriptors1 = new Mat ();

            detector.detect (img1Mat, keypoints1);
            extractor.compute (img1Mat, keypoints1, descriptors1);

            MatOfKeyPoint keypoints2 = new MatOfKeyPoint ();
            Mat descriptors2 = new Mat ();
        
            detector.detect (img2Mat, keypoints2);
            extractor.compute (img2Mat, keypoints2, descriptors2);


            DescriptorMatcher matcher = DescriptorMatcher.create (DescriptorMatcher.BRUTEFORCE_HAMMINGLUT);
            MatOfDMatch matches = new MatOfDMatch ();

            matcher.match (descriptors1, descriptors2, matches);

            Mat resultImg = new Mat ();

            Features2d.drawMatches (img1Mat, keypoints1, img2Mat, keypoints2, matches, resultImg);


            Texture2D texture = new Texture2D (resultImg.cols (), resultImg.rows (), TextureFormat.RGBA32, false);
        
            Utils.matToTexture2D (resultImg, texture);

            gameObject.GetComponent<Renderer> ().material.mainTexture = texture;
        }
    
        // Update is called once per frame
        void Update ()
        {
    
        }
    }
}
