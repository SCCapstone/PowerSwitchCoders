package com.example.user.powerswitchdemo;

import org.andengine.engine.camera.Camera;
import org.andengine.engine.handler.IUpdateHandler;
import org.andengine.engine.options.EngineOptions;
import org.andengine.engine.options.ScreenOrientation;
import org.andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
import org.andengine.entity.modifier.MoveModifier;
import org.andengine.entity.scene.Scene;
import org.andengine.entity.scene.background.IBackground;
import org.andengine.input.touch.TouchEvent;
import org.andengine.opengl.texture.TextureOptions;
import org.andengine.opengl.texture.atlas.bitmap.BuildableBitmapTextureAtlas;
import org.andengine.opengl.vbo.VertexBufferObjectManager;
import org.andengine.entity.scene.background.Background;
import org.andengine.entity.sprite.Sprite;
import org.andengine.opengl.texture.atlas.bitmap.BitmapTextureAtlas;
import org.andengine.opengl.texture.atlas.bitmap.BitmapTextureAtlasTextureRegionFactory;
import org.andengine.opengl.texture.region.ITextureRegion;
import org.andengine.ui.activity.BaseGameActivity;
import org.andengine.entity.scene.IOnSceneTouchListener;

import java.io.IOException;

/**
 * Created by User on 11/28/2017.
 * com/example/user/powerswitchdemo/GameActivity.java
 */

public class GameActivity extends BaseGameActivity {
    //this.context = context;
    BitmapTextureAtlas mBackgroundBitmapTextureAtlas;
    ITextureRegion mBackgroundTextureRegion;
    BuildableBitmapTextureAtlas levelBackground;

    //Sprite sCar;

    MoveModifier point1 = new MoveModifier(4,345,640,320,450);
    MoveModifier point2 = new MoveModifier(4,320,450,150,400);
    MoveModifier point3 = new MoveModifier(4,150,400,170,585);

    int currentPoint = 1;
    //IBackground mBackground;
    Scene currentScene;
    protected static final int CAMERA_WIDTH = 484;
    protected static final int CAMERA_HEIGHT = 804;
    BitmapTextureAtlas carTexture;
    ITextureRegion carTextureRegion;
    @Override
    public EngineOptions onCreateEngineOptions() {
        Camera mCamera = new Camera(0,0,CAMERA_WIDTH,CAMERA_HEIGHT);
        //new ratioresoution keeps ratio of resolution the same on different Android devices
        EngineOptions options = new EngineOptions(true, ScreenOrientation.PORTRAIT_FIXED, new RatioResolutionPolicy(CAMERA_WIDTH,CAMERA_HEIGHT),mCamera);
        return options;
    }

    @Override
    public void onCreateResources(OnCreateResourcesCallback pOnCreateResourcesCallback) throws IOException {
        loadGFX();
        pOnCreateResourcesCallback.onCreateResourcesFinished();
    }

    private void loadGFX() {
        //Custom load-graphics function
        //Use texture atlases instead of single images for efficiency
        BitmapTextureAtlasTextureRegionFactory.setAssetBasePath("img/");
        //Width/height must be power of 2 (2^x)
        carTexture = new BitmapTextureAtlas(getTextureManager(),64,40);
        //levelBackground = new BuildableBitmapTextureAtlas(mEngine.getTextureManager(),1024,1024, TextureOptions.BILINEAR);
        //levelBackground = new BuildableBitmapTextureAtlas(mEngine.getTextureManager(),269,392, TextureOptions.BILINEAR);
        //mBackgroundTextureRegion = BitmapTextureAtlasTextureRegionFactory.createFromAsset(levelBackground, this.getAssets(),"background.png");

        //0, 0 is top left corner reference
        carTextureRegion = BitmapTextureAtlasTextureRegionFactory.createFromAsset(carTexture,this.getAssets(),"carsmall.png",0,0);
        carTexture.load();
        // background
        //mBackgroundBitmapTextureAtlas = new BitmapTextureAtlas(this.getTextureManager(), 718, 1184,TextureOptions.NEAREST_PREMULTIPLYALPHA);
        mBackgroundBitmapTextureAtlas = new BitmapTextureAtlas(this.getTextureManager(), 400, 800,TextureOptions.NEAREST_PREMULTIPLYALPHA);
        mBackgroundTextureRegion = BitmapTextureAtlasTextureRegionFactory.createFromAsset(mBackgroundBitmapTextureAtlas, this.getAssets(), "background.png", 0, 0);

        mBackgroundBitmapTextureAtlas.load();
    }


    @Override
    public void onCreateScene(OnCreateSceneCallback pOnCreateSceneCallback) throws IOException {
        this.currentScene = new Scene();
        //todo; swap with landSAT image
        final float centerX = 241; //(CAMERA_WIDTH - this.mBackgroundTextureRegion.getWidth()) / 2;
        final float centerY = 402; //(CAMERA_HEIGHT - this.mBackgroundTextureRegion.getHeight()) / 2;
        //this.currentScene.setBackground(new Background(0,125,58));
        //this.currentScene.setBackground(mBackgroundBitmapTextureRegion);
        final Sprite spriteBG = new Sprite(centerX, centerY, this.mBackgroundTextureRegion, this.mEngine.getVertexBufferObjectManager());
        this.currentScene.attachChild(spriteBG);

        //final Sprite sCar = new Sprite(CAMERA_WIDTH/2,CAMERA_HEIGHT/2,carTextureRegion,this.mEngine.getVertexBufferObjectManager());
        final Sprite
                sCar = new Sprite(345,640,carTextureRegion,this.mEngine.getVertexBufferObjectManager());
        this.currentScene.attachChild(sCar);
        //currentScene.setOnSceneTouchListener(this.currentScene);
        currentScene.setOnSceneTouchListener(new IOnSceneTouchListener() {
            @Override
            public boolean onSceneTouchEvent(Scene pScene, TouchEvent pSceneTouchEvent) {
                if(pSceneTouchEvent.isActionDown()){
                    if (currentPoint == 1){
                        sCar.registerEntityModifier(point1);
                    }
                    if (currentPoint == 2) {
                        sCar.unregisterEntityModifier(point1);
                        sCar.registerEntityModifier(point2);
                    }
                    /*switch(currentPoint) {
                        case 1:
                            sCar.registerEntityModifier(point1);
                            //sCar.resetEntityModifiers();
                            //sCar.registerEntityModifier(point2);
                            //currentPoint = 2;
                        case 2:
                            //sCar.setX(320);
                            //sCar.setY(450);
                            //sCar.unregisterEntityModifier(point1);
                            sCar.registerEntityModifier(point2);
                            currentPoint++;
                        case 3:
                            sCar.unregisterEntityModifier(point2);
                            sCar.registerEntityModifier(point3);
                            currentPoint++;
                        case 4:
                            currentPoint = 1;
                    }*/
                    //sCar.registerEntityModifier(point1);
                }
                return false;
            }
        });

        currentScene.registerUpdateHandler(new IUpdateHandler() {
            @Override
            public void onUpdate(float pSecondsElapsed) {
                //if (currentPoint == 2) {
                //    sCar.registerEntityModifier(point2);
                    //
                //}

                if (sCar.getX() == 320 && sCar.getY() == 450){
                    currentPoint = 2;
                    sCar.unregisterEntityModifier(point1);
                    sCar.registerEntityModifier(point2);
                }
                if (sCar.getX() == 150 && sCar.getY() == 400){
                    currentPoint = 3;
                    sCar.unregisterEntityModifier(point2);
                    sCar.registerEntityModifier(point3);
                }
            }

            @Override
            public void reset() {

            }
        });

        pOnCreateSceneCallback.onCreateSceneFinished(this.currentScene);
    }

    @Override
    public void onPopulateScene(Scene pScene, OnPopulateSceneCallback pOnPopulateSceneCallback) throws IOException {

        //Sprite sCar = new Sprite(CAMERA_WIDTH/2,CAMERA_HEIGHT/2,carTextureRegion,this.mEngine.getVertexBufferObjectManager());
        //VertexBufferObjectManager vbo = this.getVertexBufferObjectManager();
        //Sprite backgroundSprite = new Sprite(0, 0 , mBackgroundTextureRegion, vbo);
        //sCar.setRotation(45.0f);
        //this.currentScene.attachChild(sCar);
        pOnPopulateSceneCallback.onPopulateSceneFinished();
    }
}
