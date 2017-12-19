package team.incorpore.dr0gis.elqueue.server.queue;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.Dictionary;
import java.util.Hashtable;

import team.incorpore.dr0gis.elqueue.R;

/**
 * Created by dr0gi on 19.12.2017.
 */

public class EventResult {
    @SerializedName("Id")
    @Expose
    private Integer id;
    @SerializedName("Name")
    @Expose
    private String name;
    @SerializedName("Type")
    @Expose
    private String type;
    @SerializedName("DateTime")
    @Expose
    private Object dateTime;
    @SerializedName("Description")
    @Expose
    private String description;
    @SerializedName("Organization")
    @Expose
    private OrganizationResult organization;

    public static Dictionary<String, Integer> Types = new Hashtable<String, Integer>() {
        {
            put("Football", R.drawable.football);
            put("Concert", R.drawable.concert);
            put("Cybersport", R.drawable.cybersport);
            put("Conference", R.drawable.conference);
        }
    };

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Object getDateTime() {
        return dateTime;
    }

    public void setDateTime(Object dateTime) {
        this.dateTime = dateTime;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public OrganizationResult getOrganization() {
        return organization;
    }

    public void setOrganization(OrganizationResult organization) {
        this.organization = organization;
    }

}
