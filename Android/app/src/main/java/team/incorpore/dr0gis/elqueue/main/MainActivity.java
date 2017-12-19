package team.incorpore.dr0gis.elqueue.main;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import team.incorpore.dr0gis.elqueue.App;
import team.incorpore.dr0gis.elqueue.authentication.AuthenticationActivity;
import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.registration.RegistrationActivity;
import team.incorpore.dr0gis.elqueue.server.ServerQuery;
import team.incorpore.dr0gis.elqueue.server.login.UserInfoResult;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;
import team.incorpore.dr0gis.elqueue.users.QueueUserList;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    private static final int MY_PERMISSIONS_REQUEST_INTERNET = 1;
    private static final int LOGIN_ACTIVITY_RESULT = 10;
    private static final int REGISTRATION_ACTIVITY_RESULT = 11;

    private List<Day> dayList = new ArrayList<>(Arrays.asList(
            new Day("Monday", "01", "January", Day.Background.WHITE),
            new Day("Tuesday", "02", "January", Day.Background.WHITE),
            new Day("Wednesday", "03", "January", Day.Background.WHITE),
            new Day("Thursday", "04", "January", Day.Background.BLACK),
            new Day("Friday", "05", "January", Day.Background.WHITE),
            new Day("Saturday", "06", "January", Day.Background.WHITE),
            new Day("Sunday", "07", "January", Day.Background.WHITE)
    ));

    /*private List<ElQueue> queueList = new ArrayList<>(Arrays.asList(
            new ElQueue("ElQueue 1", "Football match", "In Corpore Team", "Description description description", ElQueue.Type.FOOTBALL),
            new ElQueue("ElQueue 2", "Concert Louna", "In Corpore Team", "Description description description", ElQueue.Type.CONCERT),
            new ElQueue("ElQueue 3", "The international 7", "Valve", "Description description description", ElQueue.Type.CYBERSPORT),
            new ElQueue("ElQueue 4", "Blockchain", "In Corpore Team", "Description description description", ElQueue.Type.CONFERENCE),
            new ElQueue("ElQueue 5", "Football match", "In Corpore Team", "Description description description", ElQueue.Type.FOOTBALL),
            new ElQueue("ElQueue 6", "Concert Louna", "In Corpore Team", "Description description description", ElQueue.Type.CONCERT),
            new ElQueue("ElQueue 7", "The international 7", "Valve", "Description description description", ElQueue.Type.CYBERSPORT),
            new ElQueue("ElQueue 8", "Blockchain", "In Corpore Team", "Description description description", ElQueue.Type.CONFERENCE)
    ));*/
    private List<QueueResult> queueList = new ArrayList<>();

    private Menu optionMenu;

    private RecyclerView recyclerViewDay;
    private DayAdapter dayAdapter;

    private RecyclerView recyclerViewQueue;
    private QueueAdapter queueAdapter;

    private TextView tvEmailCurrentUser;
    private TextView tvStatusCurrentUser;

    private ServerQuery serverQuery;

    class OpenUsersList implements View.OnClickListener {
        private int position;

        public void setPosition(int position) {
            this.position = position;
        }

        OpenUsersList(int position) {
            this.position = position;
        }

        @Override
        public void onClick(View v) {
            Intent intent = new Intent(MainActivity.this, QueueUserList.class);
            intent.putExtra("Position", position);
            startActivity(intent);
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        recyclerViewDay = findViewById(R.id.rvDays);
        recyclerViewDay.setLayoutManager(new LinearLayoutManager(MainActivity.this, LinearLayoutManager.HORIZONTAL, false));
        dayAdapter = new DayAdapter(dayList);
        recyclerViewDay.setAdapter(dayAdapter);

        recyclerViewQueue = findViewById(R.id.rvQueues);
        recyclerViewQueue.setLayoutManager(new LinearLayoutManager(MainActivity.this, LinearLayoutManager.VERTICAL, false));
        queueAdapter = new QueueAdapter(queueList, new OpenUsersList(0));
        recyclerViewQueue.setAdapter(queueAdapter);

        serverQuery = ServerQuery.Create();

        serverQuery.getServerElQueue().getAllQueue().enqueue(new Callback<List<QueueResult>>() {
            @Override
            public void onResponse(Call<List<QueueResult>> call, Response<List<QueueResult>> response) {
                if (response.body() != null) {
                    queueList.addAll(response.body());
                    queueAdapter.notifyDataSetChanged();
                }
                else {
                    Toast.makeText(MainActivity.this, response.message(), Toast.LENGTH_LONG).show();
                    try {
                        Toast.makeText(MainActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            @Override
            public void onFailure(Call<List<QueueResult>> call, Throwable t) {

            }
        });

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        Boolean permissionInternet = ActivityCompat.checkSelfPermission(this, Manifest.permission.INTERNET) != PackageManager.PERMISSION_GRANTED;
        if (permissionInternet) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.INTERNET}, MY_PERMISSIONS_REQUEST_INTERNET);
            return;
        }

        tvEmailCurrentUser = navigationView.getHeaderView(0).findViewById(R.id.tvEmailCurrentUser);
        tvStatusCurrentUser = navigationView.getHeaderView(0).findViewById(R.id.tvStatusCurrentUser);


    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String permissions[], @NonNull int[] grantResults) {
        switch (requestCode) {
            case MY_PERMISSIONS_REQUEST_INTERNET: {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {

                } else {
                    finish();
                }
                return;
            }
            // other 'case' lines to check for other
            // permissions this app might request
        }
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        optionMenu = menu;
        getMenuInflater().inflate(R.menu.main, menu);

        changeOptionMenuOnLoginRegistration();

        return true;
    }

    private void changeOptionMenuOnLogout() {
        MenuItem login = optionMenu.findItem(R.id.authentication);
        MenuItem registration =optionMenu.findItem(R.id.registration);
        MenuItem logout = optionMenu.findItem(R.id.logout);

        login.setVisible(false);
        registration.setVisible(false);
        logout.setVisible(true);
    }
    private void changeOptionMenuOnLoginRegistration() {
        MenuItem login = optionMenu.findItem(R.id.authentication);
        MenuItem registration =optionMenu.findItem(R.id.registration);
        MenuItem logout = optionMenu.findItem(R.id.logout);

        login.setVisible(true);
        registration.setVisible(true);
        logout.setVisible(false);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK) {
            switch (requestCode) {
                case LOGIN_ACTIVITY_RESULT:
                    serverQuery.getServerElQueue().userInfo("Bearer " + serverQuery.getToken()).enqueue(new Callback<UserInfoResult>() {
                        @Override
                        public void onResponse(Call<UserInfoResult> call, final Response<UserInfoResult> response) {
                            if (response.body() != null) {
                                runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {
                                        tvEmailCurrentUser.setText(response.body().getEmail());
                                        tvStatusCurrentUser.setText((response.body().getIsAdministrator()) ? "Administrator" : "User");
                                    }
                                });
                            }
                            else {
                                Toast.makeText(MainActivity.this, response.message(), Toast.LENGTH_LONG).show();
                                try {
                                    Toast.makeText(MainActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show();
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }
                            }
                        }

                        @Override
                        public void onFailure(Call<UserInfoResult> call, Throwable t) {

                        }
                    });
                    changeOptionMenuOnLogout();
                    break;
                case REGISTRATION_ACTIVITY_RESULT:
                    Intent intent = new Intent(MainActivity.this, AuthenticationActivity.class);
                    startActivityForResult(intent, LOGIN_ACTIVITY_RESULT);
                    break;
            }
        }
        else {
            Toast.makeText(this, "Wrong result", Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement

        Intent intent;

        switch (id) {
            case R.id.authentication:
                intent = new Intent(this, AuthenticationActivity.class);
                startActivityForResult(intent, LOGIN_ACTIVITY_RESULT);
                return true;

            case R.id.registration:
                intent = new Intent(this, RegistrationActivity.class);
                startActivityForResult(intent, REGISTRATION_ACTIVITY_RESULT);
                return true;

            case R.id.logout:
                serverQuery.setToken("");
                tvEmailCurrentUser.setText(R.string.email);
                tvStatusCurrentUser.setText(R.string.status);
                changeOptionMenuOnLoginRegistration();
                return true;
        }


        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
}
