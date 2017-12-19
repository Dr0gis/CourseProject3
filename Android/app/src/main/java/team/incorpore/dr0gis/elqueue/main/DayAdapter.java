package team.incorpore.dr0gis.elqueue.main;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import team.incorpore.dr0gis.elqueue.R;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class DayAdapter extends RecyclerView.Adapter<DayHolder> {

    private List<Day> dayList;

    DayAdapter(List<Day> dayList) {
        this.dayList = dayList;
    }

    @Override
    public DayHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        LayoutInflater li = LayoutInflater.from(parent.getContext());
        View view = li.inflate(R.layout.item_day, parent, false);

        return new DayHolder(view);
    }

    @Override
    public void onBindViewHolder(DayHolder holder, int position) {
        holder.bindData(dayList.get(position));
    }

    @Override
    public int getItemCount() {
        return dayList.size();
    }
}
