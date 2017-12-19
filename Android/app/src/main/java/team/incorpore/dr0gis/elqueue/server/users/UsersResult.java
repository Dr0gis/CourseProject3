package team.incorpore.dr0gis.elqueue.server.users;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by dr0gi on 19.12.2017.
 */

public class UsersResult {
    @SerializedName("Email")
    @Expose
    private String email;
    @SerializedName("DateTimeRegistration")
    @Expose
    private String dateTimeRegistration;

    public UsersResult(String email, String dateTimeRegistration) {
        this.email = email;
        this.dateTimeRegistration = dateTimeRegistration;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getDateTimeRegistration() {
        return dateTimeRegistration;
    }

    public void setDateTimeRegistration(String dateTimeRegistration) {
        this.dateTimeRegistration = dateTimeRegistration;
    }
}
