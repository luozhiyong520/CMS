<?php

function admin_showpages( $num, $perpage, $curr_page, $mpurl, $maxpage )
{
		$multipage = "";
		$mpurl .= strpos( $mpurl, "?" ) ? "&amp;" : "?";
		if ( $perpage < $num )
		{
				$page = $maxpage;
				$offset = 2;
				$pages = ceil( $num / $perpage );
				$from = $curr_page - $offset;
				$to = $curr_page + $page - $offset - 1;
				if ( $pages < $page )
				{
						$from = 1;
						$to = $pages;
				}
				else if ( $from < 1 )
				{
						$to = $curr_page + 1 - $from;
						$from = 1;
						if ( $to - $from < $page && $to - $from < $pages )
						{
								$to = $page;
						}
				}
				else if ( $pages < $to )
				{
						$from = $curr_page - $pages + $to;
						$to = $pages;
						if ( $to - $from < $page && $to - $from < $pages )
						{
								$from = $pages - $page + 1;
						}
				}
				$multipage .= "<td align='center' class='page_redirect' style='cursor:pointer' onmouseover=\"this.className='on_page_redirect';\" onmouseout=\"this.className='page_redirect';\" onclick=\"window.location.href='".$mpurl."page=1';\" title='��ҳ'><img src='default/images/page_home.gif'></td>";
				
				for ( $i = $from;$i <= $to;	$i++	)
				{
						if ( $i != $curr_page )
						{
								$multipage .= "<td align='center' class='page_number' style='cursor:pointer' onmouseover=\"this.className='on_page_number';\" onmouseout=\"this.className='page_number';\" onclick=\"window.location.href='".$mpurl."page=".$i."';\" title='��".$i."ҳ'>".$i."</td>";
						}
						else
						{
								$multipage .= "<td align='center' class='page_curpage' title='��".$i."ҳ'>".$i."</td>";
						}
				}
				$multipage .= $page < $pages ? "<td align='center' class='page_number'>...</td><td align='center' class='page_redirect' style='cursor:pointer' onmouseover=\"this.className='on_page_redirect';\" onmouseout=\"this.className='page_redirect';\" onclick=\"window.location.href='".$mpurl."page=".$pages."';\" title='βҳ'><img src='default/images/page_end.gif'></td>" : "<td align='center' class='page_redirect' style='cursor:pointer' onmouseover=\"this.className='on_page_redirect';\" onmouseout=\"this.className='page_redirect';\" onclick=\"window.location.href='".$mpurl."page=".$pages."';\" title='βҳ'><img src='default/images/page_end.gif'></td>";
				$multipage .= "<td align='center'><input name='page' title='����ҳ�� ���س�����ת' type='text'  class='page_input' onkeypress=\"if(event.keyCode==13) window.location.href='".$mpurl."page='+value\" /></td>";
		}
		if ( !$pages )
		{
				$recordnav = "<td align='center' class='page_total' title='�ܼ�¼/ÿҳ".$perpage."��'>&nbsp;".$num."&nbsp;</td>";
		}
		else
		{
				$recordnav = "<td align='center' class='page_total' title='�ܼ�¼/ÿҳ".$perpage."��'>&nbsp;".$num."&nbsp;</td>";
				$recordnav .= "<td align='center' class='page_pages' title='��ǰҳ��/��ҳ��'>&nbsp;".$curr_page."/".$pages."&nbsp;</td>";
		}
		$tabdiv = "<div class='ChangePage mt7px'>";
		$tabdiv .= "  <div style='float:center;'>";
		$tabdiv .= "    <table border='0' cellpadding='0' cellspacing='1'>";
		$tabdiv .= "      <tr>";
		$tabdiv .= $recordnav;
		$tabdiv .= $multipage;
		$tabdiv .= "      </tr>";
		$tabdiv .= "    </table>";
		$tabdiv .= "  </div>";
		$tabdiv .= "</div>";
		return $tabdiv;
}

?>
