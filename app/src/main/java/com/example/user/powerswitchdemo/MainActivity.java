package com.example.user.powerswitchdemo;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.EditText;

import static android.provider.AlarmClock.EXTRA_MESSAGE;

public class MainActivity extends AppCompatActivity {

    public final static String EXTRA_MESSAGE = "com.example.powerswitchdemo.MESSAGE";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar myToolbar = (Toolbar) findViewById(R.id.my_toolbar);
        setSupportActionBar(myToolbar);
    }

    /** Called when the user hits the Start button */
    public void sendToSettings(View view){
        Intent new_intent = new Intent(this, SettingScreen.class);
        startActivity(new_intent);
    }

    public void sendStart(View view){
        Intent new_intent = new Intent(this, LandingScreenActivity.class);
        startActivity(new_intent);
    }
}
