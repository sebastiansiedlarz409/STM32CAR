package com.example.carapp

import android.annotation.SuppressLint
import android.content.Context
import android.content.pm.ActivityInfo
import android.hardware.Sensor
import android.hardware.SensorEvent
import android.hardware.SensorEventListener
import android.hardware.SensorManager
import android.os.Bundle
import android.os.PowerManager
import android.view.View
import android.view.Window
import android.view.WindowManager
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity(), SensorEventListener {

    private lateinit var _sensorManager : SensorManager
    private lateinit var _accelerometer : Sensor
    private lateinit var _canvas: CanvasView

    private var X: Float = 0F;
    private var Y: Float = 0F;
    private var Z: Float = 0F;

    @SuppressLint("SourceLockedOrientationActivity", "InvalidWakeLockTag")
    override fun onCreate(savedInstanceState: Bundle?) {
        requestWindowFeature(Window.FEATURE_NO_TITLE)
        super.onCreate(savedInstanceState)
        supportActionBar?.hide()

        //prepare objects
        _sensorManager = getSystemService(Context.SENSOR_SERVICE) as SensorManager
        _accelerometer = _sensorManager!!.getDefaultSensor(Sensor.TYPE_ACCELEROMETER)
        _canvas = CanvasView(this)

        //setup window
        requestedOrientation = ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE
        window.setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN)

        window.addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        window.decorView.apply {
            systemUiVisibility = View.SYSTEM_UI_FLAG_HIDE_NAVIGATION or
                    View.SYSTEM_UI_FLAG_FULLSCREEN or
                    View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
        }

        setContentView(_canvas)
    }

    override fun onAccuracyChanged(sensor: Sensor?, accuracy: Int) {

    }

    override fun onSensorChanged(event: SensorEvent?) {
        if(event != null){
            X = event.values[0];
            Y = event.values[1];
            Z = event.values[2];
            _canvas.updateMe(X, Y, Z);
        }
    }

    override fun onPause() {
        super.onPause()
        _sensorManager?.unregisterListener(this)
    }

    override fun onResume() {
        super.onResume()
        _sensorManager?.registerListener(this, _accelerometer,
            SensorManager.SENSOR_DELAY_GAME)
    }
}
