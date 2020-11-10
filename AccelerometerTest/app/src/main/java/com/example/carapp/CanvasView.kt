package com.example.carapp

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.*
import android.view.View
import androidx.core.content.res.ResourcesCompat

class CanvasView(context: Context): View(context){

    private lateinit var _extraCanvas: Canvas
    private lateinit var _extraBitmap: Bitmap

    private var Xv: Float = 0F;
    private var Yv: Float = 0F;
    private var Zv: Float = 0F;

    private var Xc: Float = 0F;
    private var Yc: Float = 0F;
    private var Zc: Float = 0F;

    private val _backgroundColor = ResourcesCompat.getColor(resources, R.color.colorBackground, null)

    init{
        Xv = (width /2).toFloat()
        Yv = (height /2).toFloat()
    }

    //this let system change view size
    override fun onSizeChanged(width: Int, height: Int, oldWidth: Int, oldHeight: Int) {
        super.onSizeChanged(width, height, oldWidth, oldHeight)

        if (::_extraBitmap.isInitialized){
            _extraBitmap.recycle()
        }

        _extraBitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888)
        _extraCanvas = Canvas(_extraBitmap)
        _extraCanvas.drawColor(_backgroundColor)
    }

    //actual draw method
    @SuppressLint("DrawAllocation")
    override fun onDraw(canvas: Canvas) {
        super.onDraw(canvas)
        canvas.drawBitmap(_extraBitmap, 0f, 0f, null)

        val paintBall = Paint()
        paintBall.style = Paint.Style.FILL
        paintBall.color = Color.parseColor("#CD5C5C")

        canvas.drawCircle(Xv, Yv, 70F, paintBall )

        val paintText = Paint()
        paintText.style = Paint.Style.FILL
        paintText.color = Color.parseColor("#FFFFFF")
        paintText.textSize = 60F

        canvas.drawText("X: $Xc", 20F, 60F, paintText)
        canvas.drawText("Y: $Yc", (width/3+20).toFloat(), 60F, paintText)
        canvas.drawText("Z: $Zc", (width/3*2+20).toFloat(), 60F, paintText)
    }

    fun updateMe(x: Float, y: Float, z:Float){
        Xv -= x;
        Yv -= y;
        Zv -= z;

        Xc = x;
        Yc = y;
        Zc = z;

        if(Xv > width){
            Xv = 0F;
        }
        if(Xv < 0){
            Xv = width.toFloat();
        }

        if(Yv > height){
            Yv = 10F;
        }
        if(Yv < 10){
            Yv = height.toFloat();
        }

        invalidate()
    }
}