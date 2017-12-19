package team.incorpore.dr0gis.elqueue;

import android.app.Application;
import android.content.Context;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class App extends Application {

    private static Context context;

    @Override
    public void onCreate() {
        super.onCreate();

        context = this;
    }

    public static Context getContext() {
        return context;
    }
}
