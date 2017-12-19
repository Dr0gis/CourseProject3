package team.incorpore.dr0gis.elqueue.authentication;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.Toast;

import java.io.IOException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.ServerQuery;
import team.incorpore.dr0gis.elqueue.server.login.LoginResult;

/**
 * Created by dr0gi on 04.12.2017.
 */

public class AuthenticationActivity extends AppCompatActivity {

    TextInputLayout tilEmail;
    TextInputLayout tilPassword;
    ServerQuery server;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_authentication);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        tilEmail = findViewById(R.id.tilEmail);
        tilPassword = findViewById(R.id.tilPassword);

        server = ServerQuery.Create();

        AppCompatButton acbLogin = findViewById(R.id.acbLogin);
        acbLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String username = tilEmail.getEditText().getText().toString();
                String password = tilPassword.getEditText().getText().toString();

                if ("".equals(username) || "".equals(password) ) {
                    return;
                }

                server.getServerElQueue().login("password", username, password).enqueue(new Callback<LoginResult>() {
                    @Override
                    public void onResponse(Call<LoginResult> call, Response<LoginResult> response) {
                        if (response.body() != null) {
                            server.setToken(response.body().getAccessToken());
                            Intent intent = new Intent();
                            setResult(RESULT_OK, intent);
                            finish();
                        }
                        else {
                            Toast.makeText(AuthenticationActivity.this, response.message(), Toast.LENGTH_LONG).show();
                            try {
                                Toast.makeText(AuthenticationActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }

                    @Override
                    public void onFailure(Call<LoginResult> call, Throwable t) {
                        Toast.makeText(AuthenticationActivity.this, "Erorr login", Toast.LENGTH_LONG).show();
                    }
                });
            }
        });

    }
}
