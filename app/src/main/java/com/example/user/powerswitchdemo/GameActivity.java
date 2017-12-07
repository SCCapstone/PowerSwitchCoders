package com.example.user.powerswitchdemo;

import org.andengine.engine.camera.Camera;
import org.andengine.engine.options.EngineOptions;
import org.andengine.engine.options.ScreenOrientation;
import org.andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
import org.andengine.entity.scene.Scene;
import org.andengine.entity.scene.background.IBackground;
import org.andengine.opengl.texture.TextureOptions;
import org.andengine.opengl.vbo.VertexBufferObjectManager;
import org.andengine.entity.scene.background.Background;
import org.andengine.entity.sprite.Sprite;
import org.andengine.opengl.texture.atlas.bitmap.BitmapTextureAtlas;
import org.andengine.opengl.texture.atlas.bitmap.BitmapTextureAtlasTextureRegionFactory;
import org.andengine.opengl.texture.region.ITextureRegion;
import org.andengine.ui.activity.BaseGameActivity;

import java.io.IOException;

/**
 * Created by User on 11/28/2017.
 * com/example/user/powerswitchdemo/GameActivity.java
 */

public class GameActivity extends BaseGameActivity {
    //this.context = context;
    BitmapTextureAtlas mBackgroundBitmapTextureAtlas;
    ITextureRegion mBackgroundTextureRegion;
    IBackground mBackground;
    Scene currentScene;
    protected static final int CAMERA_WIDTH = 800;
    protected static final int CAMERA_HEIGHT = 400;
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
        carTexture = new BitmapTextureAtlas(getTextureManager(),640,400);
        //0, 0 is top left corner reference
        carTextureRegion = BitmapTextureAtlasTextureRegionFactory.createFromAsset(carTexture,this.getAssets(),"carside.png",0,0);
        carTexture.load();
        // background
        mBackgroundBitmapTextureAtlas = new BitmapTextureAtlas(this.getTextureManager(), 718, 1184,
                TextureOptions.NEAREST_PREMULTIPLYALPHA);
        mBackgroundTextureRegion = BitmapTextureAtlasTextureRegionFactory
                .createFromAsset(mBackgroundBitmapTextureAtlas, this.getAssets(), "background480.png", 0, 0);

        mBackgroundBitmapTextureAtlas.load();
    }

    @Override
    public void onCreateScene(OnCreateSceneCallback pOnCreateSceneCallback) throws IOException {
        this.currentScene = new Scene();
        //todo; swap with landSAT image
        this.currentScene.setBackground(new Background(0,125,58));
        //this.currentScene.setBackground(mBackgroundBitmapTextureRegion);
        pOnCreateSceneCallback.onCreateSceneFinished(this.currentScene);
    }

    @Override
    public void onPopulateScene(Scene pScene, OnPopulateSceneCallback pOnPopulateSceneCallback) throws IOException {

        Sprite sCar = new Sprite(CAMERA_WIDTH/2,CAMERA_HEIGHT/2,carTextureRegion,this.mEngine.getVertexBufferObjectManager());
        //VertexBufferObjectManager vbo = this.getVertexBufferObjectManager();
        //Sprite backgroundSprite = new Sprite(0, 0 , mBackgroundTextureRegion, vbo);
        sCar.setRotation(45.0f);
        this.currentScene.attachChild(sCar);
        pOnPopulateSceneCallback.onPopulateSceneFinished();
    }
}
