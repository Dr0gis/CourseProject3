package team.incorpore.dr0gis.elqueue.server.login;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by dr0gi on 18.12.2017.
 */

public class UserInfoResult {

    @SerializedName("Email")
    @Expose
    private String email;
    @SerializedName("IsAdministrator")
    @Expose
    private Boolean isAdministrator;
    @SerializedName("HasRegistered")
    @Expose
    private Boolean hasRegistered;
    @SerializedName("LoginProvider")
    @Expose
    private Object loginProvider;

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Boolean getIsAdministrator() {
        return isAdministrator;
    }

    public void setIsAdministrator(Boolean isAdministrator) {
        this.isAdministrator = isAdministrator;
    }

    public Boolean getHasRegistered() {
        return hasRegistered;
    }

    public void setHasRegistered(Boolean hasRegistered) {
        this.hasRegistered = hasRegistered;
    }

    public Object getLoginProvider() {
        return loginProvider;
    }

    public void setLoginProvider(Object loginProvider) {
        this.loginProvider = loginProvider;
    }

}