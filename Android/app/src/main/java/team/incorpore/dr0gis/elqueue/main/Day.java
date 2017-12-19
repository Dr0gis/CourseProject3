package team.incorpore.dr0gis.elqueue.main;

import team.incorpore.dr0gis.elqueue.R;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class Day {
    private String day;
    private String numberDay;
    private String month;
    private Background background;

    enum Background {
        WHITE(R.color.color_white),
        BLACK( R.color.color_black);

        private final int id;

        Background(int id) {
            this.id = id;
        }

        public int getId() {
            return id;
        }
    }

    public Day(String day, String numberDay, String month, Background background) {
        this.day = day;
        this.numberDay = numberDay;
        this.month = month;
        this.background = background;
    }

    public String getDay() {
        return day;
    }

    public void setDay(String day) {
        this.day = day;
    }

    public String getNumberDay() {
        return numberDay;
    }

    public void setNumberDay(String numberDay) {
        this.numberDay = numberDay;
    }

    public String getMonth() {
        return month;
    }

    public void setMonth(String month) {
        this.month = month;
    }

    public Background getBackground() {
        return background;
    }

    public void setBackground(Background background) {
        this.background = background;
    }
}
