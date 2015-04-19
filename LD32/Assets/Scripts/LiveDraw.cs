using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LiveDraw : MonoBehaviour {

	public AudioSource audioSauce;
	public string CurrentAudioInput = "none";
	int deviceNum = 0;

	public float pewBand = 0f;

	public float bangBand = 0f;
	
	public bool calibratePew;
	public bool calibrateBang;
	public bool isPlaying;
	public float error;
	public float scanError;
	public Text uiText;
	public float calibrationTimer;

	void Start()
	{
		spectrum = new float[WINDOW_SIZE];
		
		pewBandSpectrum = new float[WINDOW_SIZE];
		bangBandSpectrum = new float[WINDOW_SIZE];

		string[] inputDevices = new string[Microphone.devices.Length];
		deviceNum = 0;
		
		for (int i = 0; i < Microphone.devices.Length; i++) {
			inputDevices [i] = Microphone.devices [i].ToString ();
			Debug.Log("Device: " + inputDevices [i]);
		}
		CurrentAudioInput = Microphone.devices[deviceNum].ToString();
		StartMic ();
	}
	
	public const float freq = 24000f;
	
	public void StartMic(){
		audioSauce.clip = Microphone.Start(CurrentAudioInput, true, 5, (int) freq); 
	}
	
	public void OnGUI(){
		GUI.Label (new Rect (10, 10, 400, 400), CurrentAudioInput);
	}
	
	public int WINDOW_SIZE = 512;
	
	public float[] spectrum;

	public float[] pewBandSpectrum;
	public float[] bangBandSpectrum;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Microphone.End (CurrentAudioInput);
			deviceNum+= 1;
			if (deviceNum > Microphone.devices.Length - 1)
				deviceNum = 0;
			CurrentAudioInput = Microphone.devices[deviceNum].ToString();
			
			StartMic ();
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			audioSauce.Play ();
		}
		
		float delay = 0.030f;
		int microphoneSamples = Microphone.GetPosition (CurrentAudioInput);
		//		Debug.Log ("Current samples: " + microphoneSamples);
		if (microphoneSamples / freq > delay) {
			if (!audioSauce.isPlaying) {
				Debug.Log ("Starting thing");
				audioSauce.timeSamples = (int) (microphoneSamples - (delay * freq));
				audioSauce.Play ();
			}
		}
		audioSauce.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
		int i = 1;
		if (calibratePew) {
			for(int ia=0;ia<WINDOW_SIZE;ia++)
			{
				if(pewBandSpectrum[ia]<spectrum[ia]) pewBandSpectrum[ia] = spectrum[ia];
			}
			//spectrum.CopyTo(pewBandSpectrum,0);
			calibrationTimer-=Time.deltaTime;
			uiText.text = "";
			isPlaying = true;
			calibratePew = false;
		}
		if (calibrateBang) {
			for(int ia=0;ia<WINDOW_SIZE;ia++)
			{
				if(bangBandSpectrum[ia]<spectrum[ia]) bangBandSpectrum[ia] = spectrum[ia];
			}
			//spectrum.CopyTo(pewBandSpectrum,0);
			calibrationTimer-=Time.deltaTime;
			uiText.text = "";
			isPlaying = true;
			calibrateBang = false;
		}

		if (isPlaying) {
			int pewSimilarity = 0,bangSimilarity =0;
			for(int ind=0;ind<WINDOW_SIZE;ind++)
			{
				if(spectrum[ind] >= pewBandSpectrum[ind])
					pewSimilarity++;
				if(spectrum[ind] >= bangBandSpectrum[ind])
					bangSimilarity++;
			}
			if(pewSimilarity>(WINDOW_SIZE*scanError)){uiText.text = "PEW!"+pewSimilarity; Player.Shoot (); }
			//if(bangSimilarity>(WINDOW_SIZE*scanError)) uiText.text = "BANG!"+bangSimilarity;
			else uiText.text = ""; 
		}
		if (Input.GetKey (KeyCode.Q)) {
			for(int a=0;a<WINDOW_SIZE;a++) pewBandSpectrum[a] = 0f;
			calibrationTimer = 1.5f;
			calibratePew = true;
			uiText.text = "Calibrating - say Pew";
			isPlaying = false;
		}
	//	if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.Q)) {
	//		for(int a=0;a<WINDOW_SIZE;a++) bangBandSpectrum[a] = 0f;
//calibrationTimer = 1.5f;
//calibrateBang = true;
	//		uiText.text = "Calibrating - say Bang";
	//		isPlaying = false;
	//	}
		

		while (i < WINDOW_SIZE) {
			Debug.DrawLine( new Vector3(i - 1, 50000f * spectrum[i - 1] + 10, 0), 
			               new Vector3(i, 50000f * spectrum[i] + 10, 0), 
			               Color.red);
			Debug.DrawLine( new Vector3(i - 1, 50000f * pewBandSpectrum[i - 1] + 10, 0), 
			               new Vector3(i, 50000f * pewBandSpectrum[i] + 10, 0), 
			               Color.cyan);
			Debug.DrawLine( new Vector3(i - 1, 50000f * bangBandSpectrum[i - 1] + 10, 0), 
			               new Vector3(i, 50000f * bangBandSpectrum[i] + 10, 0), 
			               Color.green);
			Debug.DrawLine( new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), 
			               new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), 
			               Color.yellow);
			i++;
		}
}
}