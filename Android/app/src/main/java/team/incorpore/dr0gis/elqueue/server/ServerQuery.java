package team.incorpore.dr0gis.elqueue.server;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.Body;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.Query;
import team.incorpore.dr0gis.elqueue.server.login.LoginResult;
import team.incorpore.dr0gis.elqueue.server.login.UserInfoResult;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;

/**
 * Created by dr0gi on 18.12.2017.
 */

public class ServerQuery {
    private static ServerQuery instance;

    private Retrofit retrofit;
    private ServerElQueue serverElQueue;
    private String urlServer;

    private String token;

    private ServerQuery() {
        urlServer = "https://0e3dc657.ngrok.io/";
        retrofit = new Retrofit.Builder().baseUrl(urlServer).addConverterFactory(GsonConverterFactory.create()).build();
        serverElQueue = retrofit.create(ServerElQueue.class);
    }

    public static ServerQuery Create() {
        if (instance == null) {
            instance = new ServerQuery();
        }
        return instance;
    }

    public ServerElQueue getServerElQueue() {
        return serverElQueue;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public interface ServerElQueue {
        @FormUrlEncoded
        @POST("/Token")
        Call<LoginResult> login(@Field("grant_type") String grantType, @Field("username") String username, @Field("password") String password);

        @FormUrlEncoded
        @POST("/api/Account/Register")
        Call<Void> registration(@Field("Email") String email, @Field("Password") String password, @Field("ConfirmPassword") String confirmPassword);

        @GET("/api/Account/UserInfo")
        Call<UserInfoResult> userInfo(@Header("Authorization") String token);

        @GET("/api/Queues/All")
        Call<List<QueueResult>> getAllQueue();
    }
}
