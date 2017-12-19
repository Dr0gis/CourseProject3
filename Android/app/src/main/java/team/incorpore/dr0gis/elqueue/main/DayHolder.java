package team.incorpore.dr0gis.elqueue.main;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Color;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.res.ResourcesCompat;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import team.incorpore.dr0gis.elqueue.App;
import team.incorpore.dr0gis.elqueue.R;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class DayHolder extends RecyclerView.ViewHolder {

    private TextView tvDay;
    private TextView tvNumberDay;
    private TextView tvMonth;
    private LinearLayout llItemDay;

    DayHolder(View itemView) {
        super(itemView);

        tvDay = itemView.findViewById(R.id.tvDay);
        tvNumberDay = itemView.findViewById(R.id.tvNumberDay);
        tvMonth = itemView.findViewById(R.id.tvMonth);
        llItemDay = itemView.findViewById(R.id.llItemDay);
    }

    void bindData(Day day) {
        tvDay.setText(day.getDay());
        tvNumberDay.setText(day.getNumberDay());
        tvMonth.setText(day.getMonth());
        llItemDay.setBackgroundResource(day.getBackground().getId());
        int color;
        if (day.getBackground().getId() == Day.Background.BLACK.getId()) {
            color = Day.Background.WHITE.getId();
        }
        else {
            color = Day.Background.BLACK.getId();
        }

        tvDay.setTextColor(ContextCompat.getColor(App.getContext(), color));
        tvNumberDay.setTextColor(ContextCompat.getColor(App.getContext(), color));
        tvMonth.setTextColor(ContextCompat.getColor(App.getContext(), color));
    }
}
