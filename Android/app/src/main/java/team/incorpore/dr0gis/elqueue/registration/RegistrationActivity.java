package team.incorpore.dr0gis.elqueue.registration;

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
import team.incorpore.dr0gis.elqueue.authentication.AuthenticationActivity;
import team.incorpore.dr0gis.elqueue.server.ServerQuery;
import team.incorpore.dr0gis.elqueue.server.login.LoginResult;

/**
 * Created by dr0gi on 05.12.2017.
 */


public class RegistrationActivity extends AppCompatActivity {

    TextInputLayout tilEmail;
    TextInputLayout tilPassword;
    TextInputLayout tilConfirmPassword;
    ServerQuery server;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_registration);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        tilEmail = findViewById(R.id.tilEmail);
        tilPassword = findViewById(R.id.tilPassword);
        tilConfirmPassword = findViewById(R.id.tilConfirmPassword);

        server = ServerQuery.Create();

        AppCompatButton acbRegistration = findViewById(R.id.acbRegistration);
        acbRegistration.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email = tilEmail.getEditText().getText().toString();
                String password = tilPassword.getEditText().getText().toString();
                String confirmPassword = tilConfirmPassword.getEditText().getText().toString();

                if ("".equals(email) || "".equals(password) ||  "".equals(confirmPassword)) {
                    return;
                }

                server.getServerElQueue().registration(email, password, confirmPassword).enqueue(new Callback<Void>() {
                    @Override
                    public void onResponse(Call<Void> call, Response<Void> response) {
                        if (response.body() != null) {
                            Intent intent = new Intent();
                            setResult(RESULT_OK, intent);
                            finish();
                        }
                        else {
                            Toast.makeText(RegistrationActivity.this, response.message(), Toast.LENGTH_LONG).show();
                            if (response.isSuccessful()) {
                                Intent intent = new Intent();
                                setResult(RESULT_OK, intent);
                                finish();
                                return;
                            }
                            try {
                                Toast.makeText(RegistrationActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }

                    @Override
                    public void onFailure(Call<Void> call, Throwable t) {
                        Toast.makeText(RegistrationActivity.this, "Erorr login", Toast.LENGTH_LONG).show();
                    }
                });
            }
        });
    }

}
